using System.Threading.Tasks;
using PokeDex.FunTranslations.Provider.Messaging;

namespace PokeDex.FunTranslations.Provider
{
	public interface IFunTranslationsProvider
	{
		public Task<TranslateResponse> TranslateAsync(TranslateRequest request);
	}
}
