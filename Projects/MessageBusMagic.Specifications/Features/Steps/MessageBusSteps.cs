using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MessageBusMagic.Specifications.Support.Fakes;
using TechTalk.SpecFlow;

namespace MessageBusMagic.Specifications.Features.Steps
{
    [Binding]
    public class MessageBusSteps
    {
        private IMessageBus MessageBus;
        private IEnumerable<Task> Publishing;
        private int ReceivedMessageCount;
        private int SubscribedToMessageCount;

        [Given(@"I have a MessageBus")]
        public void GivenIHaveAMessageBus()
        {
            MessageBus = new MessageBus();
        }

        [When(@"I subscribe to a message")]
        public void WhenISubscribeToAMessage()
        {
            MessageBus.SubscribeTo<FakeMessage>(ReceiveMessage);
            SubscribedToMessageCount++;
        }

        [When(@"I subscribe to a message multiple times")]
        public void WhenISubscribeToAMessageMultipleTimes()
        {
            for (var i = 0; i < 10; i++)
            {
                WhenISubscribeToAMessage();
            }
        }

        [When(@"I publish the message")]
        public void WhenIPublishTheMessage()
        {
            Publishing = MessageBus.Publish(new FakeMessage());
        }

        [Then(@"the message should be received by the subscriber")]
        public void ThenTheMessageShouldBeReceivedByTheSubscriber()
        {
            Task.WaitAll(Publishing.ToArray());
            ReceivedMessageCount.Should().Be(SubscribedToMessageCount);
        }

        private Task ReceiveMessage(IMessage message)
        {
            if (message.GetType() != typeof(FakeMessage))
            {
                throw new ArgumentOutOfRangeException("message", message.GetType(), string.Format("Expected message type to be FakeMessage, not {0}.", message.GetType()));
            }

            return Task.FromResult(ReceivedMessageCount++);
        }
    }
}