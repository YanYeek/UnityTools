namespace YanYeek
{

	/// <summary>
	/// 普通的单例泛型，但无法用于多线程，需要时需添加线程锁
	/// </summary>
	public class Singleton<T> where T : class, new()
	{
		private static T _instance = null;
		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new T();
				}
				return _instance;
			}
		}
	}
}