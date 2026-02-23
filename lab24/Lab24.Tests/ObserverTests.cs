using Lab24;
using Lab24.Observers;
using Xunit;

namespace Lab24.Tests
{
    public class ObserverTests
    {
        [Fact]
        public void HistoryObserver_ReceivesPublishedEvents()
        {
            var publisher = new ResultPublisher();
            var history = new HistoryLoggerObserver();
            history.Subscribe(publisher);

            publisher.PublishResult(25, "Square");
            publisher.PublishResult(64, "SquareRoot");

            Assert.Equal(2, history.History.Count);
            Assert.Contains("Operation: Square, Result: 25", history.History[0]);
            Assert.Contains("Operation: SquareRoot, Result: 64", history.History[1]);
        }

        [Fact]
        public void ThresholdObserver_NotifiesOnlyWhenExceeded()
        {
            var publisher = new ResultPublisher();
            var threshold = new ThresholdNotifierObserver(threshold: 50);
            threshold.Subscribe(publisher);

            publisher.PublishResult(49, "Square");
            Assert.False(threshold.WasNotified);

            publisher.PublishResult(51, "Square");
            Assert.True(threshold.WasNotified);
            Assert.NotNull(threshold.LastMessage);
        }
    }
}