using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using SampleBot;
using System.Collections.Generic;
using cmdMgr = CommandManager.BLL;
using FireEclectus;
using SampleBot.Entities;
using ApiAiSDK;
using ApiAiSDK.Model;

namespace FireEclectus
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private static bool hasWelcomed = false;
        private ApiAi apiAi;
        private static async Task<Rootobject> GetEntityFromLUIS(string Query)
        {
            Query = Uri.EscapeDataString(Query);
            Rootobject Data = new Rootobject();
            using (HttpClient client = new HttpClient())
            {
                string RequestURI = "https://bot.api.ai/4ed91bcf-6734-4490-8d4a-ca67e7a4926c?subscription-key=aeaa6e91911640cb89312b0de3d1ea3e&verbose=true&q=" + Query;
                HttpResponseMessage msg = await client.GetAsync(RequestURI);

                if (msg.IsSuccessStatusCode)
                {
                    var JsonDataResponse = await msg.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<Rootobject>(JsonDataResponse);
                }
            }
            return Data;
        }

        static string SiteName = string.Empty;
        static string NoOfSmokeSensors = string.Empty;
        static string NoOfHeatSensors = string.Empty;
        static string NoOfSounders = string.Empty;
        static string NoOfEvents = string.Empty;
        static int flag = 0;
        static int smokeflag = 0;
        static int heatflag = 0;
        static int soundersflag = 0;

        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            string DisplayString = string.Empty;
            var config = new AIConfiguration("aeaa6e91911640cb89312b0de3d1ea3e", SupportedLanguage.English);
            apiAi = new ApiAi(config);
            //string NoOfSmokeSensors = string.Empty;
            //string NoOfHeatSensors = string.Empty;
            //string NoOfSounders = string.Empty;
            string panelAccessCode = "pw1234";
            if (string.IsNullOrWhiteSpace(activity.Text))
            {
                var responseBotOnNULL = Request.CreateResponse(HttpStatusCode.OK);
                return responseBotOnNULL;
            }
            var response = apiAi.TextRequest(activity.Text);

            ConnectorClient connector5 = new ConnectorClient(new Uri(activity.ServiceUrl));
            if (activity.Type == ActivityTypes.Message)
            {
                Rootobject LUIS = null;//await GetEntityFromLUIS(activity.Text);

                if (LUIS == null && !cmdMgr.CommandManager.IsCommand(activity.Text))//LUIS != null && LUIS.intents != null && LUIS.intents.Count() > 0 && !cmdMgr.CommandManager.IsCommand(activity.Text))
                {
                    switch (response.Result.Metadata.IntentName)
                    {
                        case "Hi":

                            Activity reply = activity.CreateReply();
                            //            var thumbnailCard = new ThumbnailCard()
                            //            {
                            //                Title = "I'm a thumbnail card",
                            //                Subtitle = "Tachikoma hearts Robin",
                            //                Images = new List<CardImage> {
                            //    new CardImage(url: "http://robinosborne.co.uk/wp-content/uploads/2016/07/image-attachment-tachikoma.jpg")
                            //},
                            //                Buttons = new List<CardAction> {
                            //            new CardAction()
                            //              {
                            //                  Value = "https://robinosborne.co.uk/?s=bot",
                            //                  Type = "openUrl",
                            //                  Title = "Rob's bots"
                            //              }
                            //          }
                            //            };


                            List<CardImage> c0 = new List<CardImage>();
                            c0.Add(new CardImage(url: "http://insiderlouisville.com/wp-content/uploads/2014/11/mockingjay-2.jpeg"));

                            var heroCard0 = new ThumbnailCard()
                            {
                                Title = " Hello!This is Fire Mate ",
                                Subtitle = $"*Fire Panel Configuration Chat BOT*",
                                Images = c0,
                            };


                            //Attachment HC0 = heroCard0.ToAttachment();
                            //reply.Attachments.Add(HC0);

                            //reply = activity.CreateReply();
                            reply.Attachments = new List<Attachment>() { heroCard0.ToAttachment() };
                            //reply.Attachments = new List<Attachment>() { heroCard1.ToAttachment() };
                            await connector5.Conversations.ReplyToActivityAsync(reply);
                            var heroCard1 = new ThumbnailCard()
                            {
                                Title = "How would you like me to help you (whatsgoingon)",
                           
                                Subtitle = " ",
                                //Images = c1,
                                Buttons = new List<CardAction> {
                            new CardAction()
                            {
                                Value = "connect to panel",
                                Type = "imBack",
                                Title = "Connect to panel"
                            },
                             new CardAction()
                             {
                                 Value = "Open a existing configuration file",
                                 Type = "imBack",
                                 Title = "Open a existing file"
                             },
                             new CardAction()
                             {
                                 Value = "create configuration file",
                                 Type = "imBack",
                                 Title = "Create a new file"
                             }
                             },
                            };

                            //try
                            //{
                            //                reply.Attachments = new List<Attachment>() { heroCard1.ToAttachment() };
                            //                await connector5.Conversations.ReplyToActivityAsync(reply);
                            //                //            Attachment HC1 = heroCard1.ToAttachment();
                            //                //reply.Attachments.Add(HC1);

                            //            }
                            //catch (Exception ex)
                            //{

                            //    throw;
                            //}
                            // reply.Attachments = new List<Attachment>() { heroCard0.ToAttachment() };
                            reply.Attachments = new List<Attachment>() { heroCard1.ToAttachment() };
                            await connector5.Conversations.ReplyToActivityAsync(reply);
                            //DisplayString = " What would you like to do?" + "\n" + "1 create a new config file & sync to panel" + "\n" + "2 open an existing file" + "\n" + "3 connect to panel& read the data ";
                            //Activity reply3333 = activity.CreateReply(DisplayString);
                            //await connector5.Conversations.ReplyToActivityAsync(reply3333);
                            break;

                        case "ConnectToPanel":

                            DisplayString = " Can you say me know how the panel is connected  ";

                           

                            Activity reply0 = activity.CreateReply(DisplayString);

                            //list<cardimage> c30 = new list<cardimage>();
                            //c30.add(new cardimage(url: "http://insiderlouisville.com/wp-content/uploads/2014/11/mockingjay-2.jpeg"));

                            //   await connector5.Conversations.ReplyToActivityAsync(reply);

                            var heroCard30 = new ThumbnailCard()
                            {
                                Title = "Choose panel connectivity ",
                                //Subtitle = " ",
                                //Images = c1,
                                Buttons = new List<CardAction> {
                             new CardAction()
                             {
                            Value = "Bluetooth connectivity",
                            Type = "postBack",
                            Title = "Bluetooth"
                             },
                              new CardAction()
                             {
                            Value = "Wireless Fidelity",
                            Type = "imBack",
                            Title = "Wi-Fi"
                             },
                             new CardAction()
                             {
                            Value = "cloud connectivity",
                            Type = "imBack",
                            Title = "Cloud"
                             },
                             new CardAction()
                             {
                            Value = "Universal Serial Bus",
                            Type = "imBack",
                            Title = "USB"
                             }
                             },
                            };
                            reply0.Attachments = new List<Attachment>() { heroCard30.ToAttachment() };
                            //Attachment HC30 = heroCard30.ToAttachment();
                            //reply0.Attachments.Add(HC30);
                            await connector5.Conversations.ReplyToActivityAsync(reply0);
                            await cmdMgr.CommandManager.ExecuteCommand("Connect");

                            break;

                        case "Bluetooth":

                            // DisplayString = " Do you want me to list the nearby bluetooth devices?";

                            Activity reply31 = activity.CreateReply();

                            // await connector5.Conversations.ReplyToActivityAsync(reply31);

                            //  reply31 = activity.CreateReply();

                            var heroCard31 = new ThumbnailCard()
                            {
                                Title = " Do you want me to list the nearby bluetooth devices",
                                Subtitle = "         :^) ",
                                //Images = c1,
                                Buttons = new List<CardAction> {
                             new CardAction()
                             {
                            Value = "Yes, list the nearby bluetooth devices",
                            Type = "imBack",
                            Title = "Yes"
                             },
                             new CardAction()
                             {
                            Value = "do not list",
                            Type = "imBack",
                            Title = "No"
                             }
                             },
                            };
                            reply31.Attachments = new List<Attachment>() { heroCard31.ToAttachment() };
                            //Attachment HC31 = heroCard31.ToAttachment();
                            //reply31.Attachments.Add(HC31);
                            await connector5.Conversations.ReplyToActivityAsync(reply31);

                            break;

                        case "BluetoothYes":


                            DisplayString = " I found only these panels that supports Bluetooth. Choose the panel you would wish to connect";

                            Activity reply1 = activity.CreateReply(DisplayString);

                            await connector5.Conversations.ReplyToActivityAsync(reply1);

                            reply1 = activity.CreateReply();

                            reply1.Attachments = new List<Attachment>();
                            reply1.AttachmentLayout = "carousel";

                            List<CardImage> c8 = new List<CardImage>();
                            c8.Add(new CardImage(url: "https://i.ytimg.com/vi/6jGSx5oEk6s/hqdefault.jpg"));

                            List<CardImage> c9 = new List<CardImage>();
                            c9.Add(new CardImage(url: "http://www.secutron.com/media/productlinepic/MR-2200_Redm.jpg"));

                            //CardAction isa10 = new CardAction()
                            //{
                            //    //Value = "Airport",
                            //    Type = "imBack",
                            //    //Title = "morley"
                            //};

                            ThumbnailCard heroCardd15 = new ThumbnailCard()
                            {
                                //Title = "Airport",
                                Subtitle = "select any one of the way to connect ",
                                Images = c8,
                                Buttons = new List<CardAction>
                                {
                                    new CardAction()
                                         {
                                        Value = "Panel 1",
                                        Type = "imBack",
                                        Title = "Morley"
                                         },

                                    new CardAction()
                                         {
                                        Value = "Panel 2",
                                        Type = "imBack",
                                        Title = "Notifier"
                                         }
                                         },

                            };
                            //CardAction isa11 = new CardAction()
                            //{
                            //    //Value = "Hospital",
                            //    Type = "imBack",
                            //    //Title = "Hospital"
                            //};

                            //ThumbnailCard heroCardd16 = new ThumbnailCard()
                            //{

                            //    // Title = "Hospital",
                            //    Subtitle = "",
                            //    Images = c9,
                            //    Buttons = new List<CardAction>
                            //    {
                            //        new CardAction()
                            //             {
                            //            Value = "Panel 2",
                            //            Type = "imBack",
                            //            Title = "Notifier"
                            //             }
                            //             },
                            //    Tap = isa11
                            //};
                            reply1.Attachments = new List<Attachment>() { heroCardd15.ToAttachment() };
                            //reply1.Attachments = new List<Attachment>() { heroCardd16.ToAttachment() };
                            //Attachment pla10 = heroCardd15.ToAttachment();
                            //reply1.Attachments.Add(pla10);
                            //Attachment pla11 = heroCardd16.ToAttachment();
                            //reply1.Attachments.Add(pla11);

                            await connector5.Conversations.ReplyToActivityAsync(reply1);
                            break;

                        case "ChoosePanel1":

                            DisplayString = " We have made our connection with the Morley panel (handshake)";

                            Activity reply2 = activity.CreateReply(DisplayString);

                            await connector5.Conversations.ReplyToActivityAsync(reply2);
                            AccessCode:
                            reply2 = activity.CreateReply();

                            var heroCard3 = new ThumbnailCard()
                            {
                                Title = "How would you like to verify your panel? \n",
                                Subtitle = "",
                                //Images = c1,
                                Buttons = new List<CardAction> {
                             new CardAction()
                             {
                            Value = "Enter Access code",
                            Type = "imBack",
                            Title = "Enter Access code"
                             },
                             new CardAction()
                             {
                            Value = "QR code",
                            Type = "imBack",
                            Title = "Scan QR code"
                             }
                             },
                            };
                            reply2.Attachments = new List<Attachment>() { heroCard3.ToAttachment() };
                            //Attachment HC3 = heroCard3.ToAttachment();
                            //reply2.Attachments.Add(HC3);
                            await connector5.Conversations.ReplyToActivityAsync(reply2);

                            break;

                        case "AccessCode":

                            DisplayString = " Please enter access code (confidential) ";
                            Activity reply3 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply3);

                            break;

                        case "Password":

                            //int length = (LUIS.entities[0].entity).Length;
                            //if (length == 4)
                            //{
                            if (string.Compare(panelAccessCode, activity.Text) == 0)
                            {
                                DisplayString = " Access Code verified (y)";

                               
                                //   reply5 = activity.CreateReply();

                                List<CardImage> c7 = new List<CardImage>();
                                c7.Add(new CardImage(url: "https://pbs.twimg.com/profile_images/511895799614021632/Ec_tEKNf.jpeg"));
                                Activity reply66 = activity.CreateReply();
                                //reply5 = activity.CreateReply();
                                var heroCard4 = new ThumbnailCard()
                                {
                                    Title = "          Choose action ",
                                    Subtitle = " ",
                                    Images = c7,
                                    Buttons = new List<CardAction> {
                                         new CardAction()
                                         {
                                        Value = "auto read configuration",
                                        Type = "imBack",
                                        Title = "Auto read configuration"
                                         },
                                         new CardAction()
                                         {
                                        Value = "configure zones",
                                        Type = "imBack",
                                        Title = "Configure zones"
                                         },
                                          new CardAction()
                                         {
                                        Value = "configure settings",
                                        Type = "imBack",
                                        Title = "Configure settings"
                                         },
                                         new CardAction()
                                         {
                                        Value = "Assign devices",
                                        Type = "imBack",
                                        Title = "Assign devices"
                                         }
                                         },
                                };
                                reply66.Attachments = new List<Attachment>() { heroCard4.ToAttachment() };
                                //Attachment HC14 = heroCard4.ToAttachment();
                                //reply5.Attachments.Add(HC14);
                                Activity reply5 = activity.CreateReply(DisplayString);
                                await connector5.Conversations.ReplyToActivityAsync(reply5);
                                await connector5.Conversations.ReplyToActivityAsync(reply66);
                                break;
                            }
                            else
                                DisplayString = " Incorrect access code. Please try again ";
                            //}
                            //else
                            //    DisplayString = " Please enter a valid access code (stop)";

                            Activity reply4 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply4);

                            break;

                        case "AutoReadConfiguration":

                            DisplayString = " Auto read configuration completed ";
                            Activity reply6 = activity.CreateReply(DisplayString);
                            //await connector5.Conversations.ReplyToActivityAsync(reply6);
                            AutoReadConfiguration:
                            reply6 = activity.CreateReply();
                            var heroCard5 = new ThumbnailCard()
                            {
                                Title = "Choose the type of configuration ",
                                Subtitle = " ",
                                //Images = c1,
                                Buttons = new List<CardAction> {
                             new CardAction()
                             {
                            Value = "show configuration",
                            Type = "imBack",
                            Title = "show configuration"
                             },
                             new CardAction()
                             {
                            Value = "list event data",
                            Type = "imBack",
                            Title = "list event data"
                             },
                             new CardAction()
                             {
                            Value = "save to a new file",
                            Type = "imBack",
                            Title = "save to a new file"
                             },
                             },
                            };
                            reply6.Attachments = new List<Attachment>() { heroCard5.ToAttachment() };
                            //Attachment HC5 = heroCard5.ToAttachment();
                            //reply6.Attachments.Add(HC5);
                            await connector5.Conversations.ReplyToActivityAsync(reply6);

                            break;

                        case "ReadEventData":


                            Activity reply33 = activity.CreateReply(DisplayString);


                            reply33 = activity.CreateReply();
                            var heroCard33 = new ThumbnailCard()
                            {
                                Title = "How many events do you want me to pull from the panel ",
                                Subtitle = " Top intents from the panel ",
                                //Images = c1,
                                Buttons = new List<CardAction> {
                             new CardAction()
                             {
                            Value = "5 events",
                            Type = "imBack",
                            Title = "5"


                             },
                             new CardAction()
                             {
                            Value = "10 events",
                            Type = "imBack",
                            Title = "10"
                             },
                             new CardAction()
                             {
                            Value = "15 events",
                            Type = "imBack",
                            Title = "15"
                             },
                             },
                            };
                            reply33.Attachments = new List<Attachment>() { heroCard33.ToAttachment() };
                            //Attachment HC33 = heroCard33.ToAttachment();
                            //reply33.Attachments.Add(HC33);
                            await connector5.Conversations.ReplyToActivityAsync(reply33);
                            DisplayString = " Now we are trying to connect to the panel ... It may take few minutes (holdon)  ";
                            reply33 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply33);
                            break;

                        case "EventNumber":

                            //DisplayString = " Please wait (holdon)...let me pull the top 5 events from the panel ... ";
                            //await EventsResponse(activity, connector5, "ReadEvents5");
                           
                            if (activity.Text == "5 events")
                            {
                                DisplayString = " Please wait (holdon)...let me pull the top 5 events from the panel ... ";
                                await EventsResponse(activity, connector5, "ReadEvents5");

                            }
                            else if (activity.Text == "10 events")
                            {
                                DisplayString = " Please wait (holdon)...let me pull the top 10 events from the panel ... ";
                                await EventsResponse(activity, connector5, "ReadEvents10");
                                //DisplayString = await cmdMgr.CommandManager.ExecuteCommand("ReadEvents10");
                            }
                            else if (activity.Text == "15 events")
                            {
                                DisplayString = " Please wait (holdon)...let me pull the top 15 events from the panel ... ";
                                await EventsResponse(activity, connector5, "ReadEvents15");
                                //DisplayString = await cmdMgr.CommandManager.ExecuteCommand("ReadEvents15");
                            }
                            else
                            {
                                DisplayString = " Number invalid .. try only from the options ";
                            }

                            Activity reply16 = activity.CreateReply(DisplayString);

                            var heroCard32 = new ThumbnailCard()
                            {
                                Title = " Are we done or still anything remaining for today :-? ",
                                Subtitle = " ",
                                //Images = c1,
                                Buttons = new List<CardAction> {
                             new CardAction()
                             {
                            Value = "Yes, I need to perform more actions",
                            Type = "imBack",
                            Title = "Yes"
                             },
                             new CardAction()
                             {
                            Value = "no, I'm done for the day",
                            Type = "imBack",
                            Title = "No"
                             }
                             },
                            };
                            reply16.Attachments = new List<Attachment>() { heroCard32.ToAttachment() };
                            //Attachment HC34 = heroCard32.ToAttachment();
                            //reply16.Attachments.Add(HC34);
                            await connector5.Conversations.ReplyToActivityAsync(reply16);
                            reply16 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply16);
                            break;

                        case "CopyConfigurationFile":

                            Activity reply7 = activity.CreateReply();

                            reply7 = activity.CreateReply();

                            var heroCard6 = new ThumbnailCard()
                            {
                                Title = "How would you like to copy config. file ",
                                Subtitle = " ",
                                //Images = c1,
                                Buttons = new List<CardAction> {
                             new CardAction()
                             {
                            Value = "create configuration file",
                            Type = "imBack",
                            Title = "Create configuration file"
                             },
                             new CardAction()
                             {
                            Value = "Update existing configuration file",
                            Type = "imBack",
                            Title = "Update existing"
                             }
                             },
                            };
                            reply7.Attachments = new List<Attachment>() { heroCard6.ToAttachment() };
                            //Attachment HC7 = heroCard6.ToAttachment();
                            //reply7.Attachments.Add(HC7);
                            await connector5.Conversations.ReplyToActivityAsync(reply7);

                            break;

                        case "CreateConfigFile":

                            DisplayString = "What shall we call this site ";
                            Activity reply34 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply34);
                            flag = 1;
                            break;

                        case "SiteName":

                            Activity reply8 = activity.CreateReply();

                            reply8 = activity.CreateReply();

                            List<CardImage> c25 = new List<CardImage>();
                            c25.Add(new CardImage(url: "https://2.bp.blogspot.com/-D8lQd7hfU9g/UZRr0WZtx-I/AAAAAAAACN4/B0ESFC_Jkiw/s1600/gps-location.png"));


                            var heroCard7 = new ThumbnailCard()
                            {
                                Title = "        Location settings ",
                                Subtitle = " ",
                                Images = c25,
                                Buttons = new List<CardAction> {
                             new CardAction()
                             {
                            Value = "use current location",
                            Type = "imBack",
                            Title = "Use current location"
                             },
                             new CardAction()
                             {
                            Value = "skip",
                            Type = "imBack",
                            Title = "Skip"
                             }
                             },
                            };
                            reply8.Attachments = new List<Attachment>() { heroCard7.ToAttachment() };
                            //Attachment HC8 = heroCard7.ToAttachment();
                            //reply8.Attachments.Add(HC8);
                            await connector5.Conversations.ReplyToActivityAsync(reply8);

                            break;

                        case "UseCurrentLocation":

                            DisplayString = " choose the type of building";

                            Activity reply9 = activity.CreateReply(DisplayString);

                            await connector5.Conversations.ReplyToActivityAsync(reply9);

                            reply9 = activity.CreateReply();

                            reply9.Attachments = new List<Attachment>();
                            reply9.AttachmentLayout = "carousel";

                            List<CardImage> c1 = new List<CardImage>();
                            c1.Add(new CardImage(url: "https://s-media-cache-ak0.pinimg.com/736x/9d/cd/7a/9dcd7a7ca20ff4502d063a9b4e5f1d5a.jpg"));

                            List<CardImage> c2 = new List<CardImage>();
                            c2.Add(new CardImage(url: "https://s-media-cache-ak0.pinimg.com/564x/9f/51/15/9f51155934a50bfa8d790c96b891ca1a.jpg"));

                            CardAction isa = new CardAction()
                            {
                                //Value = "Airport",
                                Type = "imBack",
                                //Title = "Airport"
                            };

                            ThumbnailCard ThumbnailCardd1 = new ThumbnailCard()
                            {
                                //Title = "Airport",
                                Subtitle = "",
                                Images = c1,
                                                                Buttons = new List<CardAction>
                                {
                                    new CardAction()
                                         {
                                        Value = "Office",
                                        Type = "imBack",
                                        Title = "Airport"
                                         },
                                         
                                new CardAction()
                                {
                                    Value = "office",
                                    Type = "imBack",
                                    Title = "Hospital"
                                }
                            },
                                                                
                                 Tap = isa
                            };
                            CardAction isa1 = new CardAction()
                            {
                                // Value = "Hospital",
                                Type = "imBack",
                                // Title = "Hospital"
                            };

                            //ThumbnailCard heroCardd2 = new ThumbnailCard()
                            //{

                            //    // Title = "Hospital",
                            //    Subtitle = "",
                            //    Images = c2,
                            //    Buttons = new List<CardAction>
                            //    {
                            //        new CardAction()
                            //             {
                            //            Value = "office",
                            //            Type = "postBack",
                            //            Title = "Hospital"
                            //             }
                            //             },
                            //    Tap = isa1
                            //};
                            reply9.Attachments = new List<Attachment>() { ThumbnailCardd1.ToAttachment() };
                            //Attachment pla = ThumbnailCardd1.ToAttachment();
                            //reply9.Attachments.Add(pla);
                            //Attachment pla1 = heroCardd2.ToAttachment();
                            //reply9.Attachments.Add(pla1);

                            await connector5.Conversations.ReplyToActivityAsync(reply9);

                            break;

                        case "Office":

                            DisplayString = "(highfive) We are done with creating the project file .";
                            Activity reply35 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply35);

                            reply35 = activity.CreateReply();

                            var heroCard21 = new ThumbnailCard()
                            {
                                Title = "         Shall we add Panel. ",
                                Subtitle = "      :^)  ",
                                // Images = c21,
                                Buttons = new List<CardAction> {
                            new CardAction()
                             {
                            Value = "Yes, add panel",
                            Type = "imBack",
                            Title = "Yes"
                             },
                             new CardAction()
                             {
                            Value = "Do not add panel",
                            Type = "imBack",
                            Title = "No"
                             },
                             },
                            };
                            reply35.Attachments = new List<Attachment>() { heroCard21.ToAttachment() };
                            Attachment HC35 = heroCard21.ToAttachment();
                            reply35.Attachments.Add(HC35);
                            await connector5.Conversations.ReplyToActivityAsync(reply35);

                            break;

                        case "AddPanel":

                            Activity reply11 = activity.CreateReply();

                            reply11 = activity.CreateReply();

                            var heroCard9 = new ThumbnailCard()
                            {
                                Title = "           Choose Panel ",
                                Subtitle = "                        :^) ",
                                //Images = c1,
                                Buttons = new List<CardAction> {
                            new CardAction()
                             {
                            Value = "goto DXC Panel",
                            Type = "imBack",
                            Title = "DXC"
                             },
                             new CardAction()
                             {
                            Value = "Pluto Panel",
                            Type = "imBack",
                            Title = "ZXC"
                             },
                             new CardAction()
                             {
                            Value = "Pluto Panel",
                            Type = "imBack",
                            Title = "Pluto"
                             }
                             },
                            };
                            reply11.Attachments = new List<Attachment>() { heroCard9.ToAttachment() };
                            //Attachment HC9 = heroCard9.ToAttachment();
                            //reply11.Attachments.Add(HC9);
                            await connector5.Conversations.ReplyToActivityAsync(reply11);

                            break;

                        case "DXC":

                            Activity reply36 = activity.CreateReply();

                            reply36 = activity.CreateReply();

                            var heroCard36 = new ThumbnailCard()
                            {
                                Title = "How many loops shall we add ? ",
                                Subtitle = "                        :^) ",
                                //Images = c1,
                                Buttons = new List<CardAction> {
                            new CardAction()
                             {
                            Value = "create one panel",
                            Type = "imBack",
                            Title = "1"
                             },
                             new CardAction()
                             {
                            Value = "gotoDXC",
                            Type = "imBack",
                            Title = "2"
                             },
                             new CardAction()
                             {
                            Value = "gotoDXC",
                            Type = "imBack",
                            Title = "4"
                             }
                             },
                            };
                            reply36.Attachments = new List<Attachment>() { heroCard36.ToAttachment() };
                            //Attachment HC36 = heroCard36.ToAttachment();
                            //reply36.Attachments.Add(HC36);
                            await connector5.Conversations.ReplyToActivityAsync(reply36);

                            break;

                        case "AddOnePanel":

                            DisplayString = "How many smoke detectors are we going to add (whatsgoingon) ";
                            Activity reply37 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply37);
                            smokeflag = 1;
                            break;

                        case "HeatSensors":

                            DisplayString = "How many Heat sensors are we going to add (whatsgoingon)";
                            Activity reply38 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply38);
                            heatflag = 1;

                            break;

                        case "Sounders":

                            DisplayString = "How many Sounders are we going to add (whatsgoingon)";
                            Activity reply39 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply39);
                            soundersflag = 1;

                            break;
                        case "Byeee":

                            DisplayString = "bye...have a nice day";
                            Activity replyys = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(replyys);
                            soundersflag = 1;

                            break;

                        case "ListCreations":
                            Activity reply40 = activity.CreateReply();

                            DisplayString = "** Summary of the new list **";
                            reply40 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply40);
                            DisplayString = "Site name           : " + SiteName;
                            reply40 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply40);
                            DisplayString = "Building type      : Airport ";
                            reply40 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply40);
                            DisplayString = "Panel type          : DXC ";
                            reply40 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply40);
                            DisplayString = "No of loops        : 1 ";
                            reply40 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply40);

                            DisplayString = "Smoke sensors    : " + NoOfSmokeSensors;
                            reply40 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply40);
                            DisplayString = "Heat sensors       : " + NoOfHeatSensors;
                            reply40 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply40);
                            DisplayString = "Sounders           : " + NoOfSounders;
                            reply40 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply40);
                            reply40 = activity.CreateReply();



                            var heroCard40 = new ThumbnailCard()
                            {
                                Title = "Shall I assign default labels (whatsgoingon) ",
                                Subtitle = "                        :^) ",
                                //Images = c1,
                                Buttons = new List<CardAction> {
                            new CardAction()
                             {
                            Value = "assign default labels",
                            Type = "imBack",
                            Title = "Yes"
                             },
                            new CardAction()
                             {
                            Value = "do not assign",
                            Type = "imBack",
                            Title = "No"
                             }
                             },
                            };
                            reply40.Attachments = new List<Attachment>() { heroCard40.ToAttachment() };
                            //Attachment HC40 = heroCard40.ToAttachment();
                            //reply40.Attachments.Add(HC40);
                            await connector5.Conversations.ReplyToActivityAsync(reply40);

                            break;

                        case "UpdateLabels":

                            //DisplayString = "How shall I show you the C&E ? ";
                            Activity reply41 = activity.CreateReply();
                            //await connector5.Conversations.ReplyToActivityAsync(reply41);
                            var heroCard41 = new ThumbnailCard()
                            {
                                Title = "How shall I show you the C&E ? ",
                                //Subtitle = " ",
                                //Images = c1,
                                Buttons = new List<CardAction> {
                            new CardAction()
                             {
                            Value = "take me to oiao",
                            Type = "imBack",
                            Title = "1 in All out"
                             },
                            new CardAction()
                             {
                            Value = "gotoupdatelabels",
                            Type = "imBack",
                            Title = "2 in All out"
                             },
                            new CardAction()
                             {
                            Value = "gotoupdatelabels",
                            Type = "imBack",
                            Title = "Many in All out"
                             },
                            },
                            };
                            reply41.Attachments = new List<Attachment>() { heroCard41.ToAttachment() };
                            //Attachment HC41 = heroCard41.ToAttachment();
                            //reply41.Attachments.Add(HC41);
                            await connector5.Conversations.ReplyToActivityAsync(reply41);

                            break;

                        case "oiao":

                            DisplayString = "We have successfully completed configuration :) ";
                            Activity reply42 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply42);
                            goto case "ConnectToPanel";

                            break;

                        case "ProgramDevices":


                            DisplayString = "       Choose device";

                            Activity reply12 = activity.CreateReply(DisplayString);

                            await connector5.Conversations.ReplyToActivityAsync(reply12);

                            reply12 = activity.CreateReply();

                            reply12.Attachments = new List<Attachment>();
                            reply12.AttachmentLayout = "carousel";

                            List<CardImage> c15 = new List<CardImage>();
                            c15.Add(new CardImage(url: "https://images-na.ssl-images-amazon.com/images/I/61oTlrkCUQL._SL1200_.jpg"));

                            List<CardImage> c16 = new List<CardImage>();
                            c16.Add(new CardImage(url: "http://di3-2.shoppingshadow.com/pi/i.ebayimg.com/00/$T2eC16d,!y0E9s2S7)SLBQrsCiWpG!~~_32-260x260-0-0.JPG"));

                            CardAction isa15 = new CardAction()
                            {
                                //Value = "Airport",
                                Type = "imBack",
                                //Title = "Airport"
                            };

                            ThumbnailCard heroCard15 = new ThumbnailCard()
                            {
                                //Title = "Airport",
                                Subtitle = "",
                                Images = c15,
                                Buttons = new List<CardAction>
                                {
                                    new CardAction()
                                         {
                                        Value = "detectors",
                                        Type = "imBack",
                                        Title = "Detectors"
                                         },
                                         
                                new CardAction()
                                {
                                    Value = "strobes",
                                    Type = "imBack",
                                    Title = "Strobes"
                                }
                            },

                                Tap = isa15
                            };
                            CardAction isa16 = new CardAction()
                            {
                                // Value = "Hospital",
                                Type = "imBack",
                                // Title = "Hospital"
                            };

                            //ThumbnailCard heroCard16 = new ThumbnailCard()
                            //{

                            //    // Title = "Hospital",
                            //    Subtitle = "",
                            //    Images = c16,
                            //    Buttons = new List<CardAction>
                            //    {
                            //        new CardAction()
                            //             {
                            //            Value = "strobes",
                            //            Type = "postBack",
                            //            Title = "Strobes"
                            //             }
                            //             },
                            //    Tap = isa16
                            //};
                            //new CardAction()
                            //             {
                            //            Value = "strobes",
                            //            Type = "postBack",
                            //            Title = "Strobes"
                            //             }
                            //             },

                            reply12.Attachments = new List<Attachment>() { heroCard15.ToAttachment() };
                            //Attachment pla15 = heroCard15.ToAttachment();
                            //reply12.Attachments.Add(pla15);
                            //Attachment pla16 = heroCard16.ToAttachment();
                            //reply12.Attachments.Add(pla16);

                            await connector5.Conversations.ReplyToActivityAsync(reply12);

                            break;

                        case "Detectors":

                            Activity reply20 = activity.CreateReply();

                            reply20 = activity.CreateReply();

                            var heroCard20 = new ThumbnailCard()
                            {
                                Title = "           Choose actions ",
                                Subtitle = " ",
                                //Images = c1,
                                Buttons = new List<CardAction> {
                            new CardAction()
                             {
                            Value = "N1L1D1",
                            Type = "imBack",
                            Title = "N1L1D1"
                             },
                             new CardAction()
                             {
                            Value = "N1L1D2",
                            Type = "imBack",
                            Title = "N1L1D2"
                             },
                             new CardAction()
                             {
                            Value = "N1L1D3",
                            Type = "imBack",
                            Title = "N1L1D3"
                             },
                             new CardAction()
                             {
                            Value = "N1L1D4",
                            Type = "imBack",
                            Title = "N1L1D4"
                             }
                             },
                            };
                            reply20.Attachments = new List<Attachment>() { heroCard20.ToAttachment() };
                            //Attachment HC20 = heroCard20.ToAttachment();
                            //reply20.Attachments.Add(HC20);
                            await connector5.Conversations.ReplyToActivityAsync(reply20);


                            break;

                        case "N1L1D1":


                            Activity reply21 = activity.CreateReply();


                            List<CardImage> c21 = new List<CardImage>();
                            c21.Add(new CardImage(url: "https://www.crystalsummit.net/wp-content/uploads/sync.png"));

                            reply21 = activity.CreateReply();

                            var heroCard35 = new ThumbnailCard()
                            {
                                Title = "         Sync with the panel ",
                                Subtitle = " ",
                                Images = c21,
                                Buttons = new List<CardAction> {
                            new CardAction()
                             {
                            Value = "sync with the panel",
                            Type = "imBack",
                            Title = "Yes"
                             },
                             new CardAction()
                             {
                            Value = "Do not sync",
                            Type = "imBack",
                            Title = "No"
                             },
                             },
                            };
                            reply21.Attachments = new List<Attachment>() { heroCard35.ToAttachment() };
                            //Attachment HC21 = heroCard35.ToAttachment();
                            //reply21.Attachments.Add(HC21);
                            await connector5.Conversations.ReplyToActivityAsync(reply21);

                            break;

                        case "Synchronisation":

                            DisplayString = " All our configuration have been auto saved to the DataBase :) ... ";
                            Activity reply13 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply13);

                            reply13 = activity.CreateReply();
                            List<CardImage> c4 = new List<CardImage>();
                            c4.Add(new CardImage(url: "https://img.clipartfest.com/5460c3816cdd0b0dd4ea20a710febe6d_wave-bye-bye-cartoon-gallery-bye-animation-clipart_406-321.jpeg"));
                            reply13 = activity.CreateReply();

                            var heroCard11 = new ThumbnailCard()
                            {
                                Title = "Configuration Completed Successfully .... Bye! ",
                                Subtitle = " ",
                                Images = c4,

                            };
                            reply13.Attachments = new List<Attachment>() { heroCard11.ToAttachment() };
                            //Attachment HC13 = heroCard11.ToAttachment();
                            //reply13.Attachments.Add(HC13);
                            await connector5.Conversations.ReplyToActivityAsync(reply13);
                            break;

                        case "Exit":
                            DisplayString = " All our configuration have been auto saved to the DataBase :) ...  ";
                            Activity reply43 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply43);
                            DisplayString = "  It was pleasure assisting you . Have a nice day  ;)";
                            reply43 = activity.CreateReply(DisplayString);
                            await connector5.Conversations.ReplyToActivityAsync(reply43);

                            break;

                        default:
                            if (activity.Text == "Open a existing configuration file")
                            {
                                DisplayString = ":S I'm afraid I cant serve your request. At this point of time we support only with **\"connect to panel\"** option";
                                Activity reply17 = activity.CreateReply(DisplayString);
                                await connector5.Conversations.ReplyToActivityAsync(reply17);
                                //goto Hi;
                            }
                            else if (activity.Text == "Wireless Fidelity" || activity.Text == "cloud connectivity" || activity.Text == "Universal Serial Bus")
                            {
                                DisplayString = ":S I'm afraid I cant serve your request. At this point of time we support only with **\"Bluetooth\"** option";
                                Activity reply17 = activity.CreateReply(DisplayString);
                                await connector5.Conversations.ReplyToActivityAsync(reply17);
                                goto case "ConnectToPanel";
                            }
                            else if (activity.Text == "do not list")
                            {
                                DisplayString = " Okay then, what other connectivity ";
                                Activity reply17 = activity.CreateReply(DisplayString);
                                await connector5.Conversations.ReplyToActivityAsync(reply17);
                                goto case "ConnectToPanel";
                            }
                            else if (activity.Text == "Panel 2")
                            {
                                DisplayString = ":S I'm afraid I cant serve your request. At this point of time we support only with **\"Morley panel\"** option ";
                                Activity reply17 = activity.CreateReply(DisplayString);
                                await connector5.Conversations.ReplyToActivityAsync(reply17);
                                goto case "BluetoothYes";
                            }
                            else if (activity.Text == "QR code")
                            {
                                DisplayString = ":S I'm afraid I cant serve your request. At this point of time we support only with **\"Enter Access code\"** option ";
                                Activity reply17 = activity.CreateReply(DisplayString);
                                await connector5.Conversations.ReplyToActivityAsync(reply17);
                                goto AccessCode;
                            }
                            else if (activity.Text == "configure zones" || activity.Text == "configure settings" || activity.Text == "Assign devices")
                            {
                                DisplayString = ":S I'm afraid I cant serve your request. At this point of time we support only with **\"Auto read configuration\"** option ";
                                Activity reply17 = activity.CreateReply(DisplayString);
                                await connector5.Conversations.ReplyToActivityAsync(reply17);
                                reply17 = activity.CreateReply();
                                var heroCard4 = new ThumbnailCard()
                                {
                                    Title = "          Choose action ",
                                    Subtitle = " ",
                                    //Images = c7,
                                    Buttons = new List<CardAction> {
                                         new CardAction()
                                         {
                                        Value = "auto read configuration",
                                        Type = "imBack",
                                        Title = "Auto read configuration"
                                         },
                                         new CardAction()
                                         {
                                        Value = "configure zones",
                                        Type = "imBack",
                                        Title = "Configure zones"
                                         },
                                          new CardAction()
                                         {
                                        Value = "configure settings",
                                        Type = "imBack",
                                        Title = "Configure settings"
                                         },
                                         new CardAction()
                                         {
                                        Value = "Assign devices",
                                        Type = "imBack",
                                        Title = "Assign devices"
                                         }
                                         },
                                };
                                reply17.Attachments = new List<Attachment>() { heroCard4.ToAttachment() };
                                //Attachment HC14 = heroCard4.ToAttachment();
                                //reply17.Attachments.Add(HC14);
                                await connector5.Conversations.ReplyToActivityAsync(reply17);
                            }
                            else if (activity.Text == "show configuration" || activity.Text == "save to a new file")
                            {
                                DisplayString = ":S I'm afraid I cant serve your request. At this point of time we support only with **\"List event data\"** option ";
                                Activity reply17 = activity.CreateReply(DisplayString);
                                await connector5.Conversations.ReplyToActivityAsync(reply17);
                                goto AutoReadConfiguration;
                            }
                            else if (activity.Text == "Yes, I need to perform more actions")
                            {
                                goto AutoReadConfiguration;
                            }
                            else if (activity.Text == "no, I'm done for the day")
                            {
                                goto case "Synchronisation";
                            }
                            else if (flag == 1)
                            {
                                flag = 0;
                                SiteName = string.Copy(activity.Text);
                                goto case "SiteName";
                            }
                            else if (activity.Text == "skip")
                            {
                                goto case "Office";
                            }
                            else if (activity.Text == "Hospital")
                            {
                                DisplayString = ":S I'm afraid I cant serve your request. At this point of time we support only with **\"Airport\"** option ";
                                Activity reply17 = activity.CreateReply(DisplayString);
                                await connector5.Conversations.ReplyToActivityAsync(reply17);
                                goto case "Office";
                            }
                            else if (activity.Text == "Pluto Panel" || activity.Text == "ZXC Panel")
                            {
                                DisplayString = ":S I'm afraid I cant serve your request. At this point of time we support only with **\"DXC\"** option ";
                                Activity reply17 = activity.CreateReply(DisplayString);
                                await connector5.Conversations.ReplyToActivityAsync(reply17);
                                goto case "AddPanel";
                            }
                            else if (activity.Text == "gotoDXC")
                            {
                                DisplayString = ":S I'm afraid I cant serve your request. At this point of time we support only with **\"1\"** option ";
                                Activity reply17 = activity.CreateReply(DisplayString);
                                await connector5.Conversations.ReplyToActivityAsync(reply17);
                                goto case "DXC";
                            }
                            else if (smokeflag == 1)
                            {

                                smokeflag = 0;
                                NoOfSmokeSensors = string.Copy(activity.Text);
                                goto case "HeatSensors";
                            }
                            else if (heatflag == 1)
                            {

                                heatflag = 0;
                                NoOfHeatSensors = string.Copy(activity.Text);
                                goto case "Sounders";
                            }
                            else if (soundersflag == 1)
                            {

                                soundersflag = 0;
                                NoOfSounders = string.Copy(activity.Text);
                                goto case "ListCreations";
                            }
                            else if (activity.Text == "gotoupdatelabels")
                            {

                                DisplayString = ":S I'm afraid I cant serve your request. At this point of time we support only with **\"1 in All out\"** option ";
                                Activity reply17 = activity.CreateReply(DisplayString);
                                await connector5.Conversations.ReplyToActivityAsync(reply17);
                                goto case "UpdateLabels";
                            }
                            else
                            {
                                DisplayString = ":( Sorry, I am not getting you...try with someother options";
                                Activity reply50 = activity.CreateReply(DisplayString);
                                await connector5.Conversations.ReplyToActivityAsync(reply50);
                            }

                            /*                       ConnectorClient connectorrr = new ConnectorClient(new Uri(activity.ServiceUrl));

                                                   Activity replyyy = activity.CreateReply("What would you like to do with the panel ?");

                                                   var heroCardd = new ThumbnailCard()
                                                   {
                                                       Title = "I'm a hero card",
                                                       Subtitle = "Robin hearts Tachikoma",
                                                       Images = new List<CardImage> {
                                                       new CardImage(url: "http://robinosborne.co.uk/wp-content/uploads/2016/07/robinosborne.jpg")
                                                   },

                                                       Buttons = new List<CardAction> {
                                                    new CardAction()
                                                    {
                                                   Value = "connect to panel",
                                                   Type = "postBack",
                                                   Title = "Connect to panel"
                                                    },
                                                    new CardAction()
                                                    {
                                                   Value = "connected to panel",
                                                   Type = "postBack",
                                                   Title = "Connected to panel"
                                                    }
                                                    },
                                                   };

                                                   replyyy.Attachments = new List<Attachment> { heroCardd.ToAttachment() };

                                                   await connectorrr.Conversations.ReplyToActivityAsync(replyyy);
                          */
                            break;
                    }
                }
                else if (cmdMgr.CommandManager.IsCommand(activity.Text))
                {
                    DisplayString = await cmdMgr.CommandManager.ExecuteCommand(activity.Text);
                    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                    Activity reply = activity.CreateReply(DisplayString);

                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else
                {
                    //DisplayString = "Sorry, I am not getting you...try with someother options";
                    //Activity reply3 = activity.CreateReply(DisplayString);
                    //await connector5.Conversations.ReplyToActivityAsync(reply3);
                    DisplayString = response.Result.Metadata.IntentName;//"bye samplebot";
                    Activity replyys = activity.CreateReply(DisplayString);
                    await connector5.Conversations.ReplyToActivityAsync(replyys);

                }


                //ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                //  Activity reply = activity.CreateReply(StockRateString);

                //  await connector.Conversations.ReplyToActivityAsync(reply);


            }
            else if (cmdMgr.CommandManager.IsCommand(activity.Text))
            {
                DisplayString = await cmdMgr.CommandManager.ExecuteCommand(activity.Text);
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                Activity reply = activity.CreateReply(DisplayString);

                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                Activity reply3 = HandleSystemMessage(activity);
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                if (reply3 != null)
                    await connector.Conversations.ReplyToActivityAsync(reply3);
            }
            var responseBot = Request.CreateResponse(HttpStatusCode.OK);
            return responseBot;
        }

        private static async Task EventsResponse(Activity activity, ConnectorClient connector5, string eventName)
        {
            string json = await cmdMgr.CommandManager.ExecuteCommand(eventName);
            Activity replyToConversation = activity.CreateReply("Event Data");
            replyToConversation.Recipient = activity.From;
            replyToConversation.Type = "message";
            replyToConversation.Attachments = new List<Attachment>();
            List<StoredEvent> events = new List<StoredEvent>();
            events = ((List<StoredEvent>)JsonConvert.DeserializeObject(json, typeof(List<StoredEvent>)));
            List<ReceiptItem> receiptList = new List<ReceiptItem>();

            //ReceiptItem lineItem1 = new ReceiptItem()
            //{
            //    Title = "Event Description      Event Time  ",
            //    //Subtitle = "8 lbs",
            //    Text = null,
            //    //Image = new CardImage(url: "https://<ImageUrl1>"),
            //    //Price = "16.25",
            //    //Quantity = "1",
            //    Tap = null
            //};
            //receiptList.Add(lineItem1);
            foreach (var item in events)
            {
                ReceiptItem lineItem2 = new ReceiptItem()
                {
                    Title = item.description + " - " + item.evtDate,
                    //Subtitle = item.evtDate,
                    //Subtitle = "8 lbs",
                    Text = null,
                    //Image = new CardImage(url: "https://<ImageUrl1>"),
                    //Price = "16.25",
                    //Quantity = "1",
                    Tap = null
                };
                receiptList.Add(lineItem2);
            }



            ReceiptCard plCard = new ReceiptCard()
            {
                Title = "The requested event data is successfully fetched. Find the details here.",
                //Buttons = cardButtons,
                Items = receiptList,
                //Total = "275.25",
                //Tax = "27.52"
            };
            //Attachment plAttachment = plCard.ToAttachment();
            replyToConversation.Attachments = new List<Attachment>() { plCard.ToAttachment() };
           // replyToConversation.Attachments.Add(plAttachment);
            // var reply100 = await connector5.Conversations.SendToConversationAsync(replyToConversation);
            await connector5.Conversations.ReplyToActivityAsync(replyToConversation);
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message

            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels


            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened

            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing that the user is typing
                if (!hasWelcomed)
                {
                    hasWelcomed = true;
                    return message.CreateReply("Hello!! This is FireMate, your Fire Panel Configuration Chat BOT. How can I assist you.");
                }
            }
            else if (message.Type == ActivityTypes.Ping)
            {

            }
            else
            {
                return message.CreateReply("Hello!! This is FireMate, your Fire Panel Configuration Chat BOT. How can I assist you.");
            }

            return null;
        }
    }
}
