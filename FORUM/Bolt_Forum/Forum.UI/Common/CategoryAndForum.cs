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
    public class CategoryAndForum
    {
        private string _type_Id;
        private string _name;

        public CategoryAndForum(string type_Id, string name)
        {
            this._name = name;
            this._type_Id = type_Id;
        }

        public string Type_Id
        {
            get { return this._type_Id; }
        }
        public string Name
        {
            get { return this._name; }
        }
    }
}
