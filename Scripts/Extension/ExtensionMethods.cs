using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace YanYeek
{
    public static class ExtensionMethods
    {
        public static TaskAwaiter GetAwaiter(this UnityEngine.AsyncOperation asyncOp)
        {
            TaskCompletionSource<object> source = new TaskCompletionSource<object>();

            asyncOp.completed += obj => source.SetResult(null);

            return ((Task)source.Task).GetAwaiter();
        }
    }
}