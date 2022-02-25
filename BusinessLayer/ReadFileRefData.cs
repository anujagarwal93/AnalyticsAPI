using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayer
{
    class ReadFileRefData
    {
        private static int[][] _cashRefData;
        private static int[][] _equityRefData;
        public static int[][] ReadCashRefData()
        {
            if (_cashRefData != null && _cashRefData.Length > 0)
            {
                return _cashRefData;
            }
            string filePath = @"..\RefData\CashRefData.csv";

            using (StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open)))
            {
                _cashRefData = new int[100][];
                for (int i = 0; i < 100; i++)
                {
                    _cashRefData[i] = new int[100];
                }
                string line;
                int lineNumber = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] refData = line.Split(new char[] { ',' });
                    for (int i = 0; i < refData.Length; i++)
                    {
                        _cashRefData[lineNumber][i] = Convert.ToInt32(refData[i]);
                    }
                    lineNumber++;
                }
            }
            return _cashRefData;
        }

        public static int[][] ReadEquityRefData()
        {
            if (_equityRefData != null && _equityRefData.Length > 0)
            {
                return _equityRefData;
            }
            string filePath = @"..\RefData\EquityRefData.csv";

            using (StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open)))
            {
                _equityRefData = new int[100][];
                for (int i = 0; i < 100; i++)
                {
                    _equityRefData[i] = new int[100];
                }
                string line;
                int lineNumber = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] refData = line.Split(new char[] { ',' });
                    for (int i = 0; i < refData.Length; i++)
                    {
                        _equityRefData[lineNumber][i] = Convert.ToInt32(refData[i]);
                    }
                    lineNumber++;
                }
            }
            return _equityRefData;
        }
    }
}
