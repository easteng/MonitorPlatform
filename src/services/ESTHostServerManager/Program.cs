﻿using ESTHostServerManager;

using System;
using System.Windows.Forms;

namespace ESTHost.ServerManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
