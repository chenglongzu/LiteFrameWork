namespace LiteFramework
{
    /// <summary>
    /// ״̬����״̬
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FsmState<T> where T : class
    {
        /// <summary>
        /// ���и�״̬��״̬��
        /// </summary>
        public Fsm<T> CurrFsm;

        /// <summary>
        /// ����״̬
        /// </summary>
        internal virtual void OnEnter() { }

		/// <summary>
		/// ִ��״̬
		/// </summary>
		internal virtual void OnUpdate() { }

		/// <summary>
		/// �뿪״̬
		/// </summary>
		internal virtual void OnLeave() { }

		/// <summary>
		/// ״̬������ʱ����
		/// </summary>
		internal virtual void OnDestroy() { }

    }
}