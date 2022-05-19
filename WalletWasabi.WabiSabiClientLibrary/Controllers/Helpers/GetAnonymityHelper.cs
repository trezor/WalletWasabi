using System.Data.SQLite;
using WalletWasabi.WabiSabiClientLibrary.Models;

namespace WalletWasabi.WabiSabiClientLibrary.Controllers.Helpers;

public class GetAnonymityHelper
{
	public static GetAnonymityResponse GetAnonymity(GetAnonymityRequest request)
	{
		string databaseFilename = "Data Source=database.db";
		using (SQLiteConnection connection = new(databaseFilename))
		{
			connection.Open();

			SQLiteCommand command = new SQLiteCommand("SELECT anonymity FROM utxos WHERE txid=@txid AND vout=@vout", connection);
			command.Parameters.Add(new SQLiteParameter("@txid", request.Txid));
			command.Parameters.Add(new SQLiteParameter("@vout", request.Vout));

			using (SQLiteDataReader reader = command.ExecuteReader())
			{
				reader.Read();
				return new GetAnonymityResponse(reader.GetInt32(0));
			}
		}
	}
}
