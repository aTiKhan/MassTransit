namespace AmazonSqsReceiveEndpoint
{
    using System;
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static async Task Main()
        {
            var services = new ServiceCollection();
            services.AddMassTransit(x =>
            {
                x.UsingAmazonSqs((context, cfg) =>
                {
                    cfg.Host("us-east-2", h =>
                    {
                        h.AccessKey("your-iam-access-key");
                        h.SecretKey("your-iam-secret-key");
                    });

                    cfg.ReceiveEndpoint("input-queue", e =>
                    {
                        // disable the default topic binding
                        e.ConfigureConsumeTopology = false;

                        e.Subscribe("event-topic", s =>
                        {
                            // set topic attributes
                            s.TopicAttributes["DisplayName"] = "Public Event Topic";
                            s.TopicSubscriptionAttributes["some-subscription-attribute"] = "some-attribute-value";
                            s.TopicTags.Add("environment", "development");
                        });
                    });
                });
            });
        }
    }
}
