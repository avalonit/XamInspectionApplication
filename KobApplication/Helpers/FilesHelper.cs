using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;

namespace KobApplication
{
	public class FilesHelper
	{
		public FilesHelper()
		{
		}

		public IFile WriteFile(String fileName, String json)
		{
			return WriteJSon(fileName, json).Result;
		}

		private async Task<IFile> WriteJSon(String fileName, String json)
		{
			return await Task.Run(() => WriteJSonTask(fileName, json)).ConfigureAwait(false);
		}

		private async Task<IFile> WriteJSonTask(String fileName, String json)
		{

			IFolder folder = FileSystem.Current.LocalStorage;

			IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
			using (Stream stream1 = await file.OpenAsync(FileAccess.ReadAndWrite))
			{
				byte[] toBytes = Encoding.UTF8.GetBytes(json);
				long length = toBytes.Length;
				stream1.Write(toBytes, 0, (int)length);
			}
			return file;
		}

		public string ReadFile(IFile file)
		{
			return ReadJSon(file).Result;
		}

		private async Task<string> ReadJSon(IFile file)
		{
			return await Task.Run(() => file.ReadAllTextAsync()).ConfigureAwait(false);
		}
	}
}
