using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Collections;

namespace cz.mtrakal.ADT {
    public class BlokPrenos : IEnumerable {
        int headerSize = sizeof(int) * 2;
        FileStream fs;

        MemoryStream msBuffer;
        int blockInBuffer = -1;
        int recordsInBuffer = 0;

        public int RecordSize { get; private set; }
        public int RecordsInBlock { get; private set; }
        public int BlockSize { get; private set; }

        public BlokPrenos(string fileName, int recordsInBlock, int recordSize) {
            RecordsInBlock = recordsInBlock;
            RecordSize = recordSize;
            BlockSize = RecordsInBlock * RecordSize;

            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
            msBuffer = new MemoryStream(BlockSize);

        }
        public void Write(byte[] bytes) {
            if (bytes.Length % RecordSize != 0) {
                throw new ArgumentOutOfRangeException("Chybný vstup, jiná velikost záznamu.");
            }
            // pokud je bajtů víc, než místa v bufferu... rozdělovat
            int recordsWrited = bytes.Length / RecordSize;
            byte[] record = new byte[RecordSize];

            for (int i = 0; i < recordsWrited; i++) {
                Array.Copy(bytes, i * RecordSize, record, 0, RecordSize);
                writeRecordToBuffer(record);
            }
        }
        private void writeRecordToBuffer(byte[] bytes) {
            msBuffer.Write(bytes, 0, RecordSize);
            recordsInBuffer++;
            if (recordsInBuffer == RecordsInBlock) {
                writeBufferFile();
            }
        }
        private void writeHeader() {
            fs.Position = 0;
            List<byte> header = new List<byte>();
            header.AddRange(BitConverter.GetBytes(BlockSize));
            header.AddRange(BitConverter.GetBytes(RecordsInBlock));
            fs.Write(header.ToArray(), 0, header.Count);
        }
        private void readHeader() {
            byte[] blockSize = new byte[sizeof(int)];
            byte[] recordsInBlock = new byte[sizeof(int)];
            fs.Position = 0;
            fs.Read(blockSize, 0, sizeof(int));
            fs.Read(recordsInBlock, 0, sizeof(int));
            if (RecordsInBlock != BitConverter.ToInt32(recordsInBlock, 0)) {
                throw new IOException("File header error. Records in block expected " + RecordsInBlock + " get " + BitConverter.ToInt32(recordsInBlock, 0));
            }
            if (BlockSize != BitConverter.ToInt32(blockSize, 0)) {
                throw new IOException("File header error. Block size expected " + BlockSize + " get " + BitConverter.ToInt32(blockSize, 0));
            }
        }
        private void writeBufferFile() {
            if (fs.Length == 0) {
                writeHeader();
            }
            msBuffer.WriteTo(fs);
            fs.Flush();
            clearBuffer();
        }
        public byte[] Read(int block, int index) {
            read(block);
            Debug.Write(Path.GetFileName(fs.Name) + ": Cteni zaznamu " + index + " z bloku " + block + ".\r\n");
            byte[] data = new byte[RecordSize];
            msBuffer.Position = index * RecordSize;
            msBuffer.Read(data, 0, RecordSize);
            return data;
        }
        public byte[] Read(int block) {
            read(block);
            Debug.Write(Path.GetFileName(fs.Name) + ": Cteni bloku " + block + " z bufferu.\r\n");
            byte[] data = new byte[RecordSize * RecordsInBlock];
            msBuffer.Read(data, 0, RecordSize * RecordsInBlock);
            return data;
        }
        private void read(int block) {
            if (blockInBuffer == block) {
                return;
            }
            Debug.Write(Path.GetFileName(fs.Name) + ": Cteni bloku " + block + " do bufferu.\r\n");
            fs.Position = block * BlockSize + headerSize;
            byte[] data = new byte[BlockSize];
            fs.Read(data, 0, BlockSize);
            msBuffer.Position = 0;
            msBuffer.Write(data, 0, BlockSize);
            blockInBuffer = block;
        }
        private void clearBuffer() {
            msBuffer.Seek(0, 0);
            msBuffer.SetLength(0);
            recordsInBuffer = 0;
        }
        public void Flush() {
            if (msBuffer.Length != 0) {
                msBuffer.SetLength(msBuffer.Capacity);
            }
            writeBufferFile();
        }
        public void Close() {
            Flush();
            fs.Close();
        }
        public void ClearFile() {
            fs.Position = 0;
            fs.Seek(0, SeekOrigin.Begin);
            fs.SetLength(0);

        }
        public long GetBlockCount() {
            long count = (fs.Length - headerSize) / BlockSize;
            if (fs.Length - headerSize - count * BlockSize > 0) {
                count++;
            }
            return count;
        }
        public IEnumerator GetEnumerator() {
            for (int i = 0; i < GetBlockCount(); i++) {
                for (int j = 0; j < RecordsInBlock; j++) {
                    yield return Read(i, j);
                }
            }
        }
    }
}
