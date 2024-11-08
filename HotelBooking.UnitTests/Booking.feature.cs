﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by Reqnroll (https://www.reqnroll.net/).
//      Reqnroll Version:2.0.0.0
//      Reqnroll Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace HotelBooking.UnitTests
{
    using Reqnroll;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "2.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class CreateBookingFeature : object, Xunit.IClassFixture<CreateBookingFeature.FixtureData>, Xunit.IAsyncLifetime
    {
        
        private static global::Reqnroll.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Booking.feature"
#line hidden
        
        public CreateBookingFeature(CreateBookingFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }
        
        public static async System.Threading.Tasks.Task FeatureSetupAsync()
        {
            testRunner = global::Reqnroll.TestRunnerManager.GetTestRunnerForAssembly();
            global::Reqnroll.FeatureInfo featureInfo = new global::Reqnroll.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "", "Create Booking", "  In order to avoid double bookings\n  As a hotel manager\n  I want to ensure that " +
                    "a room can only be booked if it is available", global::Reqnroll.ProgrammingLanguage.CSharp, featureTags);
            await testRunner.OnFeatureStartAsync(featureInfo);
        }
        
        public static async System.Threading.Tasks.Task FeatureTearDownAsync()
        {
            await testRunner.OnFeatureEndAsync();
            global::Reqnroll.TestRunnerManager.ReleaseTestRunner(testRunner);
            testRunner = null;
        }
        
        public async System.Threading.Tasks.Task TestInitializeAsync()
        {
        }
        
        public async System.Threading.Tasks.Task TestTearDownAsync()
        {
            await testRunner.OnScenarioEndAsync();
        }
        
        public void ScenarioInitialize(global::Reqnroll.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public async System.Threading.Tasks.Task ScenarioStartAsync()
        {
            await testRunner.OnScenarioStartAsync();
        }
        
        public async System.Threading.Tasks.Task ScenarioCleanupAsync()
        {
            await testRunner.CollectScenarioErrorsAsync();
        }
        
        public virtual async System.Threading.Tasks.Task FeatureBackgroundAsync()
        {
#line 6
  #line hidden
            global::Reqnroll.Table table1 = new global::Reqnroll.Table(new string[] {
                        "RoomID",
                        "Description"});
            table1.AddRow(new string[] {
                        "101",
                        "Single"});
            table1.AddRow(new string[] {
                        "102",
                        "Double"});
#line 7
    await testRunner.GivenAsync("the following rooms are available", ((string)(null)), table1, "Given ");
#line hidden
            global::Reqnroll.Table table2 = new global::Reqnroll.Table(new string[] {
                        "CustomerID",
                        "Name",
                        "Email"});
            table2.AddRow(new string[] {
                        "1",
                        "John Doe",
                        "johndoe@email.com"});
            table2.AddRow(new string[] {
                        "2",
                        "Jane Smith",
                        "janesmith@email.com"});
#line 12
    await testRunner.AndAsync("the following customers are registered", ((string)(null)), table2, "And ");
#line hidden
        }
        
        async System.Threading.Tasks.Task Xunit.IAsyncLifetime.InitializeAsync()
        {
            await this.TestInitializeAsync();
        }
        
        async System.Threading.Tasks.Task Xunit.IAsyncLifetime.DisposeAsync()
        {
            await this.TestTearDownAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Successfully create a booking when the room is available")]
        [Xunit.TraitAttribute("FeatureTitle", "Create Booking")]
        [Xunit.TraitAttribute("Description", "Successfully create a booking when the room is available")]
        public async System.Threading.Tasks.Task SuccessfullyCreateABookingWhenTheRoomIsAvailable()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Successfully create a booking when the room is available", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 17
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 6
  await this.FeatureBackgroundAsync();
#line hidden
#line 18
    await testRunner.GivenAsync("customer 1 requests a booking for room 101", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 19
    await testRunner.WhenAsync("the customer tries to book from \"2025-01-10\" to \"2025-01-15\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 20
    await testRunner.ThenAsync("the booking should be successful", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Fail to create a booking when the room is not available")]
        [Xunit.TraitAttribute("FeatureTitle", "Create Booking")]
        [Xunit.TraitAttribute("Description", "Fail to create a booking when the room is not available")]
        public async System.Threading.Tasks.Task FailToCreateABookingWhenTheRoomIsNotAvailable()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Fail to create a booking when the room is not available", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 22
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 6
  await this.FeatureBackgroundAsync();
#line hidden
#line 23
    await testRunner.GivenAsync("room 102 is already booked from \"2025-01-10\" to \"2025-01-15\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 24
    await testRunner.AndAsync("customer 1 requests a booking for room 102", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 25
    await testRunner.WhenAsync("the customer tries to book from \"2025-01-10\" to \"2025-01-15\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 26
    await testRunner.ThenAsync("the booking should be unsuccessful", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "2.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : object, Xunit.IAsyncLifetime
        {
            
            async System.Threading.Tasks.Task Xunit.IAsyncLifetime.InitializeAsync()
            {
                await CreateBookingFeature.FeatureSetupAsync();
            }
            
            async System.Threading.Tasks.Task Xunit.IAsyncLifetime.DisposeAsync()
            {
                await CreateBookingFeature.FeatureTearDownAsync();
            }
        }
    }
}
#pragma warning restore
#endregion
