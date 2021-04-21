﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrowLine
{
    public interface IFigureFactory
    {
        AbstractFigure CreateFigure();
    }
}