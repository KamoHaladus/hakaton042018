﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassParser.ToParse
{
    class Person
    {
        public string Name { get; private set; }
        public Person(string name)
        {
            Name = name;
        }
        public string Speak()
        {
            return string.Format("Hello! My name is{0}",
                Name);
        }
    }
}

