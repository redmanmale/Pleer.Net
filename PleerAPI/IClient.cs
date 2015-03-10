using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PleerAPI.Common;

namespace PleerAPI
{
	interface IClient
	{
		void Init();

		TokenInfo GetTokenInfo();

		[Description("Поиск треков")]
		TrackList TracksSearch(String query, int page = 1, int resultOnPage = 10, string quality = Quality.All);

		[Description("Получить информацию о треке")]
		TrackInfoRs GetTrackInfo(String trackId);

		[Description("Получить текст трека")]
		TrackLyrics GetTrackLyrics(String trackId);

		[Description("Получить ссылку на загрузку")]
		TrackUrl GetTrackDownloadLink(String trackId, String reason = Reason.Listen);

		[Description("Получить топ за период")]
		TopTrackList GetTopList(int listType, int page, String language);

		[Description("Получить автодополнение")]
		SuggestList GetSuggest(String part);
	}
}
