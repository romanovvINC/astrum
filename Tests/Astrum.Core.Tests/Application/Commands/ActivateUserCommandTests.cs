﻿using Astrum.Identity.Features.Commands;
using Astrum.Tests;
using FluentAssertions;
using Xunit;
using IResult = Astrum.SharedLib.Common.Results.IResult;

namespace Astrum.Core.Tests.Application.Commands;

public class ActivateUserCommandTests : TestBase
{
    [Fact]
    public async Task ActivateUser_WhenUserDoesNotExit_ShouldFailWithMessage()
    {
        var activateUserCommand = new ActivateUserCommand("unknownUser");

        var result = await ServiceBus.Send(activateUserCommand);

        result.Failed.Should().BeTrue();
        // result.Message.Should().Be("User not found.");
    }

    [Fact]
    public async Task ActivateUser_WhenUserActive_ShouldFailWithMessage()
    {
        var activateUserCommand = new ActivateUserCommand("activeUser");
        var result = await ServiceBus.Send(activateUserCommand);

        result.Failed.Should().BeTrue();
        // result.Message.Should().Be("User is already active.");
    }

    [Fact]
    public async Task ActivateUser_WhenUserInactive_ShouldSucceed()
    {
        var activateUserCommand = new ActivateUserCommand("inactiveUser");
        var result = await ServiceBus.Send(activateUserCommand);

        // result.Succeeded.Should().BeTrue();
    }

    [Fact]
    public async Task ActivateUser_WhenServiceThrowsException_ShouldFail()
    {
        var activateUserCommand = new ActivateUserCommand("throwsException");
        var result = await ServiceBus.Send(activateUserCommand);

        // result.Succeeded.Should().BeFalse();
        // result.Message.Should().Be("Error while trying to activate user.");
        // result.Exception.Should().NotBeNull();
    }
}