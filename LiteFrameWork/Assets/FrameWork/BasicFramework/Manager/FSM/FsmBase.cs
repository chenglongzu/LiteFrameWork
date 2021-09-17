using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiteFramework
{
    /// <summary>
    /// ״̬������
    /// </summary>
    public abstract class FsmBase
    {
        /// <summary>
        /// ״̬�����
        /// </summary>
        public int FsmId { get; private set; }

        /// <summary>
        /// ��ǰ״̬������
        /// </summary>
        public sbyte CurrStateType;

        public FsmBase(int fsmId)
        {
            FsmId = fsmId; 
        }

        /// <summary>
        /// �ر�״̬��
        /// </summary>
        public abstract void ShutDown();

    }
}