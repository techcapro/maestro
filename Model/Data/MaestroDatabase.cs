using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Maestro
{
	public class MaestroDatabase
	{
		readonly SQLiteAsyncConnection database;

		public MaestroDatabase(string dbPath)
		{
			database = new SQLiteAsyncConnection(dbPath);
			database.CreateTableAsync<AlertNewsFeed>().Wait();
		}


		/// <summary>
		/// Gets the items async.
		/// </summary>
		/// <returns>The items async.</returns>
		public Task<List<AlertNewsFeed>> GetItemsAsync()
		{
			return database.Table<AlertNewsFeed>().ToListAsync();
		}

		/// <summary>
		/// Gets the item async.
		/// </summary>
		/// <returns>The item async.</returns>
		/// <param name="obj">Object.</param>
		public Task<AlertNewsFeed> GetItemAsync(AlertNewsFeed obj)
		{
			return database.Table<AlertNewsFeed>().Where(i => i.Id == obj.Id && i.AppliedEntityId == obj.AppliedEntityId).FirstOrDefaultAsync();
		}

		//public Task<List<TodoItem>> GetItemsNotDoneAsync()
		//{
		//	return database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
		//}


		/// <summary>
		/// Saves the item async.
		/// </summary>
		/// <returns>The item async.</returns>
		/// <param name="item">Item.</param>
		public async Task<List<AlertNewsFeed>> SaveItemAsync(AlertNewsFeed item)
		{
			var itemRequestExistInDB = await GetItemAsync(item) as AlertNewsFeed;
			if (itemRequestExistInDB != null
				&& itemRequestExistInDB.Id == item.Id
				&& itemRequestExistInDB.AppliedEntityId == item.AppliedEntityId)
			{
				itemRequestExistInDB.IsRead = item.IsRead;
				itemRequestExistInDB.IsSaved = item.IsSaved;
				await database.UpdateAsync(itemRequestExistInDB);
			}
			else {
				await database.InsertAsync(item);
			}

			return await GetItemsAsync();
		}


		/// <summary>
		/// Deletes the item async.
		/// </summary>
		/// <returns>The item async.</returns>
		/// <param name="item">Item.</param>
		public async Task<List<AlertNewsFeed>> DeleteItemAsync(AlertNewsFeed item)
		{
			var itemRequestExistInDB = await GetItemAsync(item);
			if (itemRequestExistInDB != null
				&& itemRequestExistInDB.Id == item.Id
				&& itemRequestExistInDB.AppliedEntityId == item.AppliedEntityId)
			{
				await database.DeleteAsync(itemRequestExistInDB);
			}

			return await GetItemsAsync();
		}
	}
}
