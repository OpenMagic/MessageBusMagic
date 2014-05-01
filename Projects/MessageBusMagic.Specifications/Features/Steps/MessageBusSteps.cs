using TechTalk.SpecFlow;

namespace MessageBusMagic.Specifications.Features.Steps
{
    [Binding]
    public class MessageBusSteps
    {
        [Given(@"I have a MessageBus")]
        public void GivenIHaveAMessageBus()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I subscribe to a message")]
        public void WhenISubscribeToAMessage()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I publish the message")]
        public void WhenIPublishTheMessage()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the message should be received by the subscriber")]
        public void ThenTheMessageShouldBeReceivedByTheSubscriber()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
