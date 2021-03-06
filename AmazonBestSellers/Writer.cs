﻿/* Copyright (c) David T Robertson 2016 */
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AmazonBestSellers
{
    /// <summary>
    /// Writes ISBNs to a file. This is used by the DomainSlim class to flush output as categories are processed.
    /// </summary>
    public static class Writer
    {
        private static string Filepath;
        private static object locker = new Object();

        static Writer()
        {
            try
            {
                lock (locker)
                {
                    DateTime datetime = DateTime.Now;
                    string formatedDate = datetime.ToString("MM.dd.yy H.mm.ss");
                    if (Form1.outputDirectory != null)
                    {
                        Filepath = string.Format("{0}All_ISBN_{1}.txt", Form1.outputDirectory, formatedDate);
                    }
                    else
                    {
                        Filepath = string.Format("Results\\All_ISBN_{0}.txt", formatedDate);
                    }
                    (new FileInfo(Filepath)).Directory.Create();
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error creating output file", ex);
                throw ex;
            }
        }

        public static void WriteToFile(List<string> text)
        {
            try
            {
                lock (locker)
                {
                    using (FileStream file = new FileStream(Filepath, FileMode.Append, FileAccess.Write, FileShare.Read))
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        foreach(string t in text)
                        {
                            writer.Write(t);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error writing to output file", ex);
            }

        }
    }
}