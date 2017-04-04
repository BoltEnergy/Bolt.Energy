#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
namespace Com.Comm100.Framework.ASPNETState
{
    public class SessionOperator
    {
        private int _operatorId;
        private double _timezoneOffset;

        public SessionOperator(int operatorId, double timezoneOffset)
        {
            this._operatorId = operatorId;
            this._timezoneOffset = timezoneOffset;
        }

        public int OperatorId
        {
            get { return this._operatorId; }
        }

        public double TimezoneOffset
        {
            get { return this._timezoneOffset; }
        }
    }
}
