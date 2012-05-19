var settingsAjaxTimeout = 1000;
$(document).ready(function (event) {
    logPageEvent(getEvent('document', 'ready'));
    $(window).load(windowLoadHandler);
    $(window).scroll(windowScrollHandler);
    $(window).resize(windowResizeHandler);
    $(document).click(clickHandler);
    $(document).mousemove(mouseMoveHandler);
    $('*').scroll(scrollHandler);

    $("input").each(attachHandlers);
    $("select").each(attachHandlers);
});

function getPosEvent(id, eventType, elementType, posX, posY) {
    return { 'id': id, 'eventType': eventType, 'elementType': elementType, 'x': posX, 'y': posY };
}

// Used primarilly for window or document events where no
// state, like a value or position, is kept.
function getEvent(id, eventType, elementType) {
    return getPosEvent(id, eventType, elementType, null, null);
}

function windowLoadHandler(event) {
    logPageEvent(getEvent('window', event.type, 'window'));
}

function mouseMoveHandler(event) {
    var posEvent = getPosEvent(event.target.id, event.type, event.target.type, event.pageX, event.pageY);
    logPageEvent(posEvent);
}

function windowResizeHandler(event) {
    var posEvent = getPosEvent('window', event.type, 'window', $(window).height(), $(this).width());
    logPageEvent(posEvent);
}

function windowScrollHandler(event) {
    var posEvent = getPosEvent('window', event.type, 'window', $(window).scrollLeft(), $(this).scrollTop());
    logPageEvent(posEvent);
}

function scrollHandler(event) {
    var posEvent = getPosEvent(event.target.id, event.type, event.target.type, event.target.scrollLeft, event.target.scrollTop); nt(event.target.id, event.type, event.target.type, event.target.scrollLeft, event.target.scrollTop); nt(event.target.id, event.type, event.target.type, event.target.scrollLeft, event.target.scrollTop);
    logPageEvent(posEvent);
}

function clickHandler(event) {
    var posEvent = getPosEvent(event.target.id, event.type, event.target.type, event.pageX, event.pageY);
    logPageEvent(posEvent);
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

function logFormEvent(formEvent) {
    logEventToDiv(formEvent);
    var millisSince1970 = new Date().getTime();
    $.ajax({
        url: getFormTrackingUrl(),
        type: 'Post',
        data: {
            VisitGuid: getVisitGuid(),
            Url: getUrl(),
            PageId: getPageId(),
            ElementId: formEvent.id,
            EventType: formEvent.eventType,
            ElementType: formEvent.elementType,
            ElementValue: formEvent.elementValue,
            ValueValid: formEvent.valid,
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

function logPageEvent(pageEvent) {
    logEventToDiv(pageEvent);
    var millisSince1970 = new Date().getTime();
    $.ajax({
        url: getPageTrackingUrl(),
        type: 'Post',
        data: {
            VisitGuid: getVisitGuid(),
            Url: getUrl(),
            PageId: getPageId(),
            ElementId: pageEvent.id,
            EventType: pageEvent.eventType,
            ElementType: pageEvent.elementType,
            X: pageEvent.x,
            Y: pageEvent.y,
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

function getUrl() {
    return window.location.href;
}

function getVisitGuid() {
    return $('#VisitGuid').val();
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

function jsonToString(obj) {
    var result = '';
    for (var key in obj) {
        if (obj.hasOwnProperty(key)) {
            result += key + " = " + obj[key] + ", ";
        }
    }
    return result;
}

function logEventToDiv(obj) {
    var row = jsonToString(obj);
    $('#events').html(new Date().getTime() + ' ' + row + "<br/>" + $('#events').html());
}