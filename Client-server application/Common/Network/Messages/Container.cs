namespace Common.Network.Messages
{
    public static class Container
    {
        public static MessageContainer GetContainer(string identifier, object payload)
        {
            var container = new MessageContainer
            {
                Identifier = identifier,
                Payload = payload
            };

            return container;
        }
    }
}
