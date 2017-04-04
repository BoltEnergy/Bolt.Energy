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
    public interface ISignaturePermission
    {
        int MaxLengthofSignature { get; }

        /*--------Signature content permission added 5.13 by Allon.--------*/
        //bool IfSignatureAllowHTML { get; }
        bool IfSignatureAllowUrl { get; }
        bool IfSignatureAllowInsertImage { get; }
        /*---------------*/
    }
}
