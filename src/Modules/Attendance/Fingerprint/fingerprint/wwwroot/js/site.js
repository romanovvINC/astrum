// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var userTimeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;
document.cookie = "userTimeZone=" + userTimeZone + "; path=/";