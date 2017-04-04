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
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Com.Comm100.Framework.Enum;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Framework.Database;
using Com.Comm100.Forum.DataAccess;

namespace Com.Comm100.Forum.Bussiness
{
    public class PollOptionStruct
    {
        private int _id;
        private string _optionText;
        private int _orderNum;

        public int Id
        {
            get { return this._id; }
        }
        public string OptionText
        {
            get { return this._optionText; }
        }
        public int OrderNum
        {
            get { return this._orderNum; }
        }

        public PollOptionStruct(int id, string optionText, int orderNum)
        {
            _id = id;
            _optionText = optionText;
            _orderNum = orderNum;
        }
    }
}
