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

namespace Com.Comm100.Forum.Bussiness
{
    public interface IAttachmentPermission
    {
        bool IfAllowUploadAttachment { get; }
        int MaxCountOfAttacmentsForOnePost { get; }
        int MaxSizeOfOneAttachment { get; }//unit:Kbyte
        int MaxSizeOfAllAttachments { get; }//unit:Kbyte
    }
}
