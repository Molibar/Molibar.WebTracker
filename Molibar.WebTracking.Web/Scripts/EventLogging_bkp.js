startElementLoadTimer();
var settingsAjaxTimeout = 1000;
$(document).ready(function () {
    startPageTimer();
    $(window).load(logPageLoad);
    $("input").each(attatchHandlers);
    $("select").each(attatchHandlers);
});

function attatchHandlers() {
    if ($(this).context.type == 'hidden' || $(this).context.id == '') return;
    $(this).change(function () {
        var time = getTime();
        var valid = true;
        logEvent(event.type, $(this).context.type, $(this).context.id, $(this).val(), valid, time);
    });
    $(this).focusin(function () {
        startTimer();
    });
    $(this).focusout(function () {
        timer = null;
    });
}

var pageTimer;
function startPageTimer() {
    pageTimer = new Date();
}

function getPageLoadTime() {
    var time = new Date() - pageTimer;
    pageTimer = null;
    return time;
}

var elementLoadTimer;
function startElementLoadTimer() {
    elementLoadTimer = new Date();
}

function getElementLoadTime() {
    var time = new Date() - elementLoadTimer;
    elementLoadTimer = null;
    return time;
}

var timer;
function startTimer() {
    timer = new Date();
}

function getTime() {
    if (time == null) return 0;
    var time = new Date() - timer;
    timer = null;
    return time;
}

function logPageLoad() {
    var pageLoadTime = getPageLoadTime();
    logEvent('PageLoad', 'Page', 'Page', null, 'true', pageLoadTime);
    var elementLoadTime = getElementLoadTime();
    logEvent('ElementLoad', 'Page', null, 'true', elementLoadTime);
}

function logEvent(event, elementType, elementId, elementValue, valueValid, timeInMillis) {
    $.ajax({
        url: 'http://localhost:50342/Api/Tracking/FormEvent',
        type: 'Post',
        data: {
            VisitGuid: getVisitGuid(),
            Url: getUrl(),
            PageId: getPageId(),
            EventName: event,
            ElementType: elementType,
            ElementId: elementId,
            ElementValue: elementValue,
            ValueValid: valueValid,
            TimeInMillis: timeInMillis
        },
        success: function (response, status, xhr) {
        },
        error: function (e, jqxhr, settings, exception) {
        },
        timeout: settingsAjaxTimeout
    });
    return false;
}

function getVisitGuid() {
    return $('#VisitGuid').val();
}

function getUrl() {
    return window.location.href;
}
function getPageId() {
    var pageId = $('#PageId').val();
    if ((pageId == null) || (pageId.length == 0)) return getUrl();
    return pageId;
}
