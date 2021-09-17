using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LiteFramework
{
	/// <summary>
	/// ״̬��������
	/// </summary>
	public class FsmManager : BaseManager<FsmManager>, IDisposable
	{
		/// <summary>
		/// ״̬���ֵ�
		/// </summary>
		private Dictionary<int, FsmBase> m_FsmDic;

		/// <summary>
		/// ״̬������ʱ���
		/// </summary>
		private int m_TemFsmId = 0;

		public void Dispose()
		{
			var enumerator = m_FsmDic.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.Value.ShutDown();
			}
			m_FsmDic.Clear();
		}
		internal void Init()
		{
			m_FsmDic = new Dictionary<int, FsmBase>();

		}

		/// <summary>
		/// ����״̬��
		/// </summary>
		/// <typeparam name="T">ӵ��������</typeparam>
		/// <param name="fsmId">״̬�����</param>
		/// <param name="owner">ӵ����</param>
		/// <param name="states">״̬����</param>
		/// <returns></returns>
		public Fsm<T> Create<T>(T owner, FsmState<T>[] states) where T : class
		{
			Fsm<T> fsm = new Fsm<T>(m_TemFsmId++, owner, states);
			m_FsmDic[m_TemFsmId] = fsm;
			return fsm;
		}

		/// <summary>
		/// ����״̬��
		/// </summary>
		/// <param name="fsmId">״̬�����</param>
		public void DestoryFsm(int fsmId)
		{
			FsmBase fsm = null;
			if (m_FsmDic.TryGetValue(fsmId, out fsm))
			{
				fsm.ShutDown();
				m_FsmDic.Remove(fsmId);
			}
		}

		
	}
}