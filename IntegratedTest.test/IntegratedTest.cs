//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.TestHost;
//using Newtonsoft.Json;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Linq;
//using Google_Keep.Models;
//using Google_Keep.Controllers;
//using Xunit;
//using FluentAssertions;
//using System.Net.Http.Headers;
//using System.Text;
//using System;
//using Google_Keep;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.DependencyInjection;
//using System.Net;
//using Microsoft.AspNetCore.StaticFiles;
//using Microsoft.AspNetCore.HttpsPolicy;
//using Microsoft.AspNetCore.Diagnostics;

//namespace Google_KeepIntegratedtest.test
//{
//    public class NotesControllerIntegrationTests
//    {
//        private readonly TestServer _server;
//        private readonly HttpClient _client;
//        private Google_KeepContext _context;

//        public NotesControllerIntegrationTests()
//        {
//            _server = new TestServer(new WebHostBuilder().UseEnvironment("Testing").UseStartup<Startup>());
//            _client = _server.CreateClient();
//            _client.DefaultRequestHeaders.Accept.Clear();
//            _context = _server.Host.Services.GetService(typeof(Google_KeepContext)) as Google_KeepContext;
//            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//            List<Note> note = new List<Note>
//            {
//                new Note
//                {
//                    id = 1,
//                    title = "My First Note",
//                    plain_text = "This is my plaintext",
//                    IsPinned = true,
//                    checks = new List<Checklist>()
//                        {
//                        new Checklist()
//                        {
//                            checklist="checklist data 1"
//                        }
//                    },
//                    label = new List<Label>()
//                    {
//                        new Label()
//                        {
//                            label ="Nikita"
//                        }
//                    }
//                },
//                new Note
//                {
//                    id = 2,
//                    title = "My First Note1",
//                    plain_text = "This is my plaintext1",
//                    IsPinned = true,
//                    checks = new List<Checklist>()
//                    {
//                        new Checklist()
//                        {
//                            checklist="checklist data 1"
//                        }
//                    },
//                label = new List<Label>()
//                    {
//                        new Label()
//                        {
//                            label ="Nikita"
//                        }
//                    }
//                },
//                new Note
//                {
//                    id = 3,
//                    title = "My First Note2",
//                plain_text = "This is my plaintext2",
//                IsPinned = true,
//                checks = new List<Checklist>()
//                    {
//                        new Checklist()
//                        {
//                            checklist="checklist data 1"
//                        }
//                    },
//                label = new List<Label>()
//                    {
//                        new Label()
//                        {
//                            label ="Nikita"
//                        }
//                    }
//                }
//            };
//            _context.Note.AddRange(note);
//            _context.SaveChanges();


//        }
//        [Fact]
//        public async Task GetAllNotes()
//        {
//            // Act
//            var response = await _client.GetAsync("api/Notes1");

//            // response.EnsureSuccessStatusCode();
//            var responseString = await response.Content.ReadAsStringAsync();
//            var note = JsonConvert.DeserializeObject<List<Note>>(responseString);
//            //   notes.Count().Should().Be(3);
//            //var okobj = notes.Value as List<Notes>;
//            Assert.Equal(3, note.Count());

//            // Assert
//        }

//        [Fact]
//        public async Task Get()
//        {
//            // Act
//            var response = await _client.GetAsync("api/Notes1/2");

//            // Assert
//            response.EnsureSuccessStatusCode();
//            var responseString = await response.Content.ReadAsStringAsync();
//            var note = JsonConvert.DeserializeObject<Note>(responseString);

//            note.id.Should().Be(2);
//        }

//        [Fact]
//        public async Task PostNotes()
//        {
//            Note note = new Note()
//            {
//                // Arrange


//                id = 4,
//                title = "My First Note",
//                plain_text = "This is my plaintext",
//                IsPinned = true,
//                checks = new List<Checklist>()
//                        {
//                            new Checklist()
//                            {
//                                checklist="checklist data 1"
//                            }
//                        },
//                label = new List<Label>()
//                        {
//                            new Label()
//                            {
//                                label ="Nikita"
//                            }
//                        }

//            };

//            var content = JsonConvert.SerializeObject(note);
//            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
//            var response = await _client.PostAsync("api/Notes1", stringContent);
//            response.EnsureSuccessStatusCode();
//            var responseString = await response.Content.ReadAsStringAsync();
//            var notetopost = JsonConvert.DeserializeObject<Note>(responseString);
//            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
//        }
//        [Fact]
//        public async Task PostInvalid()
//        {
//            // Arrange
//            var note = new Note { title = "John" };
//            var content = JsonConvert.SerializeObject(note);
//            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
//            // Act
//            var response = await _client.PostAsync("/api/Notes1", stringContent);
//            // Assert
//            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
//        }
//        [Fact]
//        public async Task PutInvalid()
//        {
//            // Arrange
//            var note = new Note { title = "John" };
//            var content = JsonConvert.SerializeObject(note);
//            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

//            // Act
//            var response = await _client.PutAsync("/api/Notes1/16", stringContent);

//            // Assert
//            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
//            var responseString = await response.Content.ReadAsStringAsync();
//        }
//        [Fact]
//        public async Task PutNotes()
//        {
//            // Arrange
//            List<Note> note = new List<Note>
//            {
//                new Note
//                {
//                id = 3,
//                title = "John",
//                plain_text = "Doe",
//                IsPinned = false,
//                checks = new List<Checklist>()
//                    {
//                        new Checklist()
//                        {
//                            checklist="checklist data 5"
//                        }
//                    },
//                label = new List<Label>()
//                    {
//                        new Label()
//                        {
//                            label ="Boeing"
//                        }
//                    }
//                }
//            };
//            var content = JsonConvert.SerializeObject(note);
//            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

//            // Act
//            var response = await _client.PutAsync("api/Notes1/EDIT/3", stringContent);
//            var responseStringbyput = await response.Content.ReadAsStringAsync();
//            var notebyput = JsonConvert.DeserializeObject<Note>(responseStringbyput);
//            var responsebyget = await _client.GetAsync("api/Notes1/3");
//            var responseStringbyget = await responsebyget.Content.ReadAsStringAsync();
//            var notebyget = JsonConvert.DeserializeObject<Note>(responseStringbyget);
//            // Assert.Equal(notes.title, "John");
//            //  responseString.Should().Be(String.Empty);
//        }

//        [Fact]
//        public async Task DeleteNotes()
//        {
//            // Act
//            var response = await _client.DeleteAsync("api/Notes1/2");

//            // Assert
//            response.EnsureSuccessStatusCode();
//            var responseString = await response.Content.ReadAsStringAsync();
//            var responsenote = JsonConvert.DeserializeObject<Note>(responseString);
//            //responseString.Should().Be(String.Empty);
//            Assert.Equal(responsenote.id, 2);
//        }

//    }
//}