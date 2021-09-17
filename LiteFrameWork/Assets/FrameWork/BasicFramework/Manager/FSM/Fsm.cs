using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiteFramework
{
	/// <summary>
	/// ״̬��
	/// </summary>
	/// <typeparam name="T">FSMManager</typeparam>
	public class Fsm<T> : FsmBase where T : class
	{
		/// <summary>
		/// ״̬��ӵ����
		/// </summary>
		public T Owner { get; private set; }

		/// <summary>
		/// ��ǰ״̬
		/// </summary>
		private FsmState<T> m_CurrState;

		/// <summary>
		/// ״̬�ֵ�
		/// </summary>
		private Dictionary<sbyte, FsmState<T>> m_StateDic;

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="fsmId">״̬�����</param>
		/// <param name="owner">ӵ����</param>
		/// <param name="states">״̬����</param>
		public Fsm(int fsmId, T owner, FsmState<T>[] states) : base(fsmId)
		{
			m_StateDic = new Dictionary<sbyte, FsmState<T>>();

			Owner = owner;

			//��״̬�����ֵ�
			int len = states.Length;
			for (int i = 0; i < len; i++)
			{
				FsmState<T> state = states[i];
				state.CurrFsm = this;
				m_StateDic[(sbyte)i] = state;
			}

			//����Ĭ��״̬
			CurrStateType = -1;
		}

		/// <summary>
		/// ��ȡ״̬
		/// </summary>
		/// <param name="stateType">״̬Type</param>
		/// <returns>״̬</returns>
		public FsmState<T> GetState(sbyte stateType)
		{
			FsmState<T> state = null;
			m_StateDic.TryGetValue(stateType, out state);
			return state;
		}

		internal void OnUpdate()
		{
			if (m_CurrState != null)
			{
				m_CurrState.OnUpdate();
			}
		}

		/// <summary>
		/// �л�״̬
		/// </summary>
		/// <param name="newState"></param>
		public void ChangeState(sbyte newState)
		{
			if (CurrStateType == newState) return;

			if (m_CurrState != null)
			{
				m_CurrState.OnLeave();
			}
			CurrStateType = newState;
			m_CurrState = m_StateDic[CurrStateType];

			//������״̬
			m_CurrState.OnEnter();
		}

		/// <summary>
		/// �ر�״̬��
		/// </summary>
		public override void ShutDown()
		{
			if (m_CurrState != null)
			{
				m_CurrState.OnLeave();
			}

			foreach (KeyValuePair<sbyte, FsmState<T>> state in m_StateDic)
			{
				state.Value.OnDestroy();
			}
			m_StateDic.Clear();
		}
	}
}