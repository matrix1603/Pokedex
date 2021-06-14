namespace PokeDex.Infrastructure.Cache
{
	public interface ICacheStorage
	{
		void Remove(string key);
		void Store(string key, int cacheTimeInMinutes, object data);
		T Retrieve<T>(string key);
	}
}
