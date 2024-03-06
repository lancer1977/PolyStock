﻿using System;

namespace PolyStock.Data
{
    public interface IChange
    {
        DateTime date { get; }
        int cases { get; }
        int? deaths { get; }
        string state { get; }
        string county { get;  }
        int? fips { get; }
    }
}
