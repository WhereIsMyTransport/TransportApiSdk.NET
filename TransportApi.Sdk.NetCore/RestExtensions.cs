using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TransportApi.Sdk.NetCore
{
    public static class RestExtensions
    {
        public static async Task<IRestResponse<T>> ExecuteTaskAsync<T>(this RestClient client, IRestRequest request, CancellationToken token)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            TaskCompletionSource<IRestResponse<T>> taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();

            try
            {
                RestRequestAsyncHandle async = client.ExecuteAsync<T>(
                    request,
                    (response, _) =>
                    {
                        if (token.IsCancellationRequested)
                        {
                            taskCompletionSource.TrySetCanceled();
                        }
                        // Don't run TrySetException, since we should set Error properties and swallow exceptions
                        // to be consistent with sync methods
                        else
                        {
                            taskCompletionSource.TrySetResult(response);
                        }
                    });


                CancellationTokenRegistration registration =

                    token.Register(() =>
                    {
                        async.Abort();
                        taskCompletionSource.TrySetCanceled();
                    });


                taskCompletionSource.Task.ContinueWith(t => registration.Dispose(), token);

            }
            catch (Exception ex)
            {
                taskCompletionSource.TrySetException(ex);
            }

            return await taskCompletionSource.Task;
        }
    }
}
