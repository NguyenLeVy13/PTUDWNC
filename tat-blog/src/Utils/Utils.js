export function isEmptyOrSpaces(str) {
    return str == null || (typeof str === 'string' && str.match(/^ *$/) !==
null);
}