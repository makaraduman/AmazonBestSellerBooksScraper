﻿/* Copyright (c) David T Robertson 2016 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AmazonBestSellers
{
    /// <summary>
    /// This class is used to maintain a count of the total number of books added throughout the process. It is
    /// also used to count the number of finished domains.
    /// </summary>
    static class Counter
    {
        private static long _booksAdded;
        public static long BooksAdded 
        { 
            get
            {
                return _booksAdded;
            }
        }
        private static int _finished;
        public static int Finished
        {
            get
            {
                return _finished;
            }
        }

        static Counter()
        {
            _finished = 0;
            _booksAdded = 0;
        }
        public static void Reset()
        {
            _finished = 0;
            _booksAdded = 0;
        }
        public static void IncrementBooksAdded(int newBooks)
        {
            Interlocked.Add(ref _booksAdded, newBooks);
        }
        public static void IncrementFinished()
        {
            Interlocked.Increment(ref _finished);
        }
    }
}
