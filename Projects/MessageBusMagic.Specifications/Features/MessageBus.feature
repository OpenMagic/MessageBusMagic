Feature: MessageBus
	In order to develop decoupled applications
	As a programmer
	I want a MessageBus to subscribe to & publish messages 

Scenario: Subscribe to a message
	Given I have a MessageBus
	When I subscribe to a message
	And I publish the message
	Then the message should be received by the subscriber

Scenario: Subscribe to a message multiple times
	Given I have a MessageBus
	When I subscribe to a message multiple times
	And I publish the message
	Then the message should be received by the subscriber

# todo: Scenario: Subscribe to a message multiple times
# todo: Scenario: Subscribe to a message with a handler multiple times.