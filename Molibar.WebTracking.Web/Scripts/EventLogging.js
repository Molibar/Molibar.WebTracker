var settingsAjaxTimeout = 1000;
$(document).ready(function (event) {
    logEventToDiv(getEvent('document', 'ready'));
    $(window).load(windowLoadHandler);
    $(window).scroll(windowScrollHandler);
    $(window).resize(windowResizeHandler);
    $(document).click(clickHandler);
    $('*').scroll(scrollHandler);

    $("input").each(attachHandlers);
    $("select").each(attachHandlers);
});


function getPos(id, eventType, elementType, posX, posY) {
    return { 'id': id, 'eventType': eventType, 'elementType': elementType, 'x': posX, 'y': posY };
}

function getEvent(id, eventType, elementType) {
    return getPos(id, eventType, elementType, null, null);
}

function clickHandler(event) {
    logEventToDiv(getPos(event.target.id, event.type, event.target.type, event.pageX, event.pageY));
}

function windowLoadHandler(event) {
    logEventToDiv(getEvent('window', event.type, 'window'));
}

function windowResizeHandler(event) {
    logEventToDiv(getPos('window', event.type, 'window', $(window).height(), $(this).width()));
}

function windowScrollHandler(event) {
    logEventToDiv(getPos('window', event.type, 'window', $(window).scrollLeft(), $(this).scrollTop()));
}

function scrollHandler(event) {
    logEventToDiv(getPos(event.target.id, event.type, event.target.type, event.target.scrollLeft, event.target.scrollTop));
}

function getFormEvent(id, eventType, type, value, valid) {
    return { 'id': id, 'eventType': eventType, 'elementType': type, 'elementValue': value, 'valid': valid };
}

function getValue(element) {
    if (element.context.type == "checkbox") return element.context.checked;
    else return element.val();
}

function logElementEvent(element, event) {
    var valid = true;
    var req = getFormEvent(element.context.id, event.type, element.context.type, getValue(element), valid, true);
    logFormEvent(req);
    logEventToDiv(req);
}

function attachHandlers() {
    if ($(this).context.type == 'hidden' || $(this).context.id == '') return;
    $(this).change(function (event) {
        logElementEvent($(this), event);
    });
    $(this).focusin(function (event) {
        logElementEvent($(this), event);
    });
    $(this).focusout(function (event) {
        logElementEvent($(this), event);
    });
    $(this).blur(function (event) {
        logElementEvent($(this), event);
    });
}

function logEventToDiv(obj) {
    var row = jsonToString(obj);
    $('#events').html(new Date().getTime() + ' ' + row + "<br/>" + $('#events').html());
}

function jsonToString(obj) {
    var result = '';
    for (var key in obj) {
        if (obj.hasOwnProperty(key)) {
            result += key + " = " + obj[key] + ", ";
        }
    }
    return result;
}

function logFormEvent(req) {
    var millisSince1970 = new Date().getTime();
    $.ajax({
        url: getFormTrackingUrl(),
        type: 'Post',
        data: {
            VisitGuid: getVisitGuid(),
            Url: getUrl(),
            PageId: getPageId(),
            ElementId: req.id,
            EventType: req.eventType,
            ElementType: req.elementType,
            ElementValue: req.elementValue,
            ValueValid: req.valid,
            MillisSince1970: millisSince1970
        },
        success: function (response, status, xhr) {
        },
        error: function (e, jqxhr, settings, exception) {
        },
        timeout: settingsAjaxTimeout
    });
    return false;
}

function logPageEvent(req) {
    var millisSince1970 = new Date().getTime();
    $.ajax({
        url: getPageTrackingUrl(),
        type: 'Post',
        data: {
            VisitGuid: getVisitGuid(),
            Url: getUrl(),
            PageId: getPageId(),
            ElementId: req.id,
            EventType: req.eventType,
            ElementType: req.elementType,
            X: req.x,
            Y: req.y,
            MillisSince1970: millisSince1970
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

function getFormTrackingUrl() {
    return $('#FormTrackerUrl').val();
}

function getPageTrackingUrl() {
    return $('#PageTrackerUrl').val();
}

function getPageId() {
    var pageId = $('#PageId').val();
    if ((pageId == null) || (pageId.length == 0)) return '';
    return pageId;
}