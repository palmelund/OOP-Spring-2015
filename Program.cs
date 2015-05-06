﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Spring_2015
{
    class Program
    {
        public static void Main()
        {
            IStringSystem stringSystem = new IStringSystem();
            StringSystemCLI cli = new StringSystemCLI(stringSystem);
            StringSystemCommandParser parser = new StringSystemCommandParser(cli, stringSystem);
            cli.Start(parser);
        }
    }
}
