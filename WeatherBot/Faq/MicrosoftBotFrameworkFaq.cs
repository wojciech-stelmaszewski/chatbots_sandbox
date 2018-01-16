﻿using System;
using System.Collections.Generic;

namespace WeatherBot.Faq
{
    public class MicrosoftBotFrameworkFaq
    {
        private static readonly IDictionary<string, string> Faq = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "WhyDeveloped", "While the Conversation User Interface (CUI) is upon us, at this point few developers have the expertise and tools needed to create new conversational experiences or enable existing applications and services with a conversational interface their users can enjoy. We have created the Bot Framework to make it easier for developers to build and connect great bots to users, wherever they converse, including on Microsoft's premier channels." },
            { "ChannelsWhen", "We plan on making continuous improvements to the Bot Framework, including additional channels, but cannot provide a schedule at this time. If you would like a specific channel added to the framework, let us know." },
            { "NewChannel", "We have not provided a general mechanism for developers to add new channels to Bot Framework, but you can connect your bot to your app via the Direct Line API. If you are a developer of a communication channel and would like to work with us to enable your channel in the Bot Framework we’d love to hear from you." },
            { "SkypeBot", "The Bot Framework is designed to build, connect, and deploy high quality, responsive, performant and scalable bots for Skype and many other channels. The SDK can be used to create text/sms, image, button and card-capable bots (which constitute the majority of bot interactions today across conversation experiences) as well as bot interactions which are Skype-specific such as rich audio and video experiences. If you already have a great bot and would like to reach the Skype audience, your bot can easily be connected to Skype (or any supported channel) via the Bot Builder for REST API (provided it has an internet-accessible REST endpoint)." },
            { "PersonalInfo", "Each bot is its own service, and developers of these services are required to provide Terms of Service and Privacy Statements per their Developer Code of Conduct. You can access this information from the bot’s card in the Bot Directory. To provide the I/O service, the Bot Framework transmits your message and message content (including your ID), from the chat service you used to the bot." },
            { "RemoveBot", "Users have a way to report a misbehaving bot via the bot’s contact card in the directory. Developers must abide by Microsoft terms of service to participate in the service." },
            { "FirewallConfig", "You'll need to whitelist the following URLs in your corporate firewall: login.botframework.com (Bot authentication) login.microsoftonline.com (Bot authentication) westus.api.cognitive.microsoft.com (for Luis.ai NLP integration) state.botframework.com (Bot state storage for prototyping) cortanabfchanneleastus.azurewebsites.net (Cortana channel) cortanabfchannelwestus.azurewebsites.net (Cortana Channel) *.botFramework.com (channels)" },
            { "RateLimiting", "The Bot Framework service must protect itself and its customers against abusive call patterns (e.g., denial of service attack), so that no single bot can adversely affect the performance of other bots. To achieve this kind of protection, we’re adding rate limits (also known as throttling) to all endpoints. By enforcing a rate limit, we can restrict the frequency with which a bot can make a specific call. For example: with rate limiting enabled, if a bot wanted to post a large number of activities, it would have to space them out over a time period." },
            { "RateLimitInpacted ", "It is unlikely you’ll experience rate limiting, even at high volume. Most rate limiting would only occur due to bulk sending of activities (from a bot or from a client), extreme load testing, or a bug. When a request is throttled, an HTTP 429 (Too Many Requests) response is returned along with a Retry-After header indicating the amount of time (in seconds) to wait before retrying the request would succeed. You can collect this information by enabling analytics for your bot via Azure Application Insights. Or, you can add code in your bot to log messages." },
            { "RateLimitConditions", "It can happen if: a bot sends messages too frequently a client of a bot sends messages too frequently Direct Line clients request a new Web Socket too frequently" },
            { "WhatRateLimits ", "We’re continuously tuning the rate limits to make them as lenient as possible while at the same time protecting our service and our users. Because thresholds will occasionally change, we aren’t publishing the numbers at this time. If you are impacted by rate limiting, feel free to reach out to us at bf-reports@microsoft.com." },
            { "CognitiveServices", "Both the Bot Framework and Cognitive Services are new capabilities introduced at Microsoft Build 2016 that will also be integrated into Cortana Intelligence Suite at GA. Both these services are built from years of research and use in popular Microsoft products. These capabilities combined with ‘Cortana Intelligence’ enable every organization to take advantage of the power of data, the cloud and intelligence to build their own intelligent systems that unlock new opportunities, increase their speed of business and lead the industries in which they serve their customers." },
            { "CortanaIntelligence ", "Cortana Intelligence is a fully managed Big Data, Advanced Analytics and Intelligence suite that transforms your data into intelligent action. It is a comprehensive suite that brings together technologies founded upon years of research and innovation throughout Microsoft (spanning advanced analytics, machine learning, big data storage and processing in the cloud) and: Allows you to collect, manage and store all your data that can seamlessly and cost effectively grow over time in a scalable and secure way. Provides easy and actionable analytics powered by the cloud that allow you to predict, prescribe and automate decision making for the most demanding problems. Enables intelligent systems through cognitive services and agents that allow you to see, hear, interpret and understand the world around you in more contextual and natural ways. With Cortana Intelligence, we hope to help our enterprise customers unlock new opportunities, increase their speed of business and be leaders in their industries." },
            { "DirectLine", "Direct Line is a REST API that allows you to add your bot into your service, mobile app, or webpage. You can write a client for the Direct Line API in any language. Simply code to the Direct Line protocol, generate a secret in the Direct Line configuration page, and talk to your bot from wherever your code lives. Direct Line is suitable for: Mobile apps on iOS, Android, and Windows Phone, and others Desktop applications on Windows, OSX, and more Webpages where you need more customization than the embeddable Web Chat channel offers Service-to-service applications" }
        };

        public static string FindAnAnswer(string question)
        {
            if (!String.IsNullOrWhiteSpace(question))
            {
                var trimmedQuestion = question.Trim();
                if (Faq.ContainsKey(trimmedQuestion))
                {
                    return Faq[trimmedQuestion];
                }
            }

            return "Unfortunately, I don't know the answer for that!";
        }
    }
}