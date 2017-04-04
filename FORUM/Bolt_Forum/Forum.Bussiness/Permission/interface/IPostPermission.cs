﻿#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Comm100.Forum.Bussiness
{
    public interface IPostPermission
    {
        bool IfAllowPost { get; }
        bool IfPostNotNeedModeration { get; }
        int MinIntervalForPost { get; } //unit: second
        int MaxLengthOfPost { get; }
    }
}
