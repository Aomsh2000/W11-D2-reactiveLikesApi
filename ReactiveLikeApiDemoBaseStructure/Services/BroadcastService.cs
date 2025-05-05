using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ReactiveLikeApiDemo.Models;

namespace ReactiveLikeApiDemo.Services
{
    public class BroadcastService
    {
        // Defines a 2-second delay to limit how often updates are pushed for each post.
        private static readonly TimeSpan BroadcastInterval = TimeSpan.FromSeconds(2);

        // A stream that groups posts by ID and rate-limits updates using Reactive Extensions (Rx).
        private static readonly IConnectableObservable<Post> _rateLimitedStream;

        static BroadcastService()
        {
            _rateLimitedStream = DataStore.PostSubject
                .GroupBy(post => post.Id) // Groups incoming updates by post ID.
                .SelectMany(group =>
                    group.Throttle(BroadcastInterval)) // Only takes the latest update every 2 seconds.
                .Publish();  // Makes the observable hot (shared among subscribers).

            _rateLimitedStream.Connect(); // Starts emitting values to subscribers.

        }

        public static IDisposable Subscribe(Action<Post> onNext)
        {
            // Allows consumers to receive rate-limited post updates.
            return _rateLimitedStream.Subscribe(onNext);
            
        }
    }
}