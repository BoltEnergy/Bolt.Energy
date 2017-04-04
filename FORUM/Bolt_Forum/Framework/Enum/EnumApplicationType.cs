
#if OPENSOURCE
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

namespace Com.Comm100.Framework.Enum
{
    public enum EnumApplicationType
    {
        enumAdmin = 0,        
        enumLiveChat = 1,
        enumForum = 2,
        enumNewsletter = 4,
        enumKnowledgeBase = 8,
        enumEmailTicket = 16,
        enumPartner = 32,
    }
}
