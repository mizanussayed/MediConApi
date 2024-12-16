document.addEventListener("DOMContentLoaded", function () {
    $("#loader").fadeOut();
});

window._helper = {
    date: {
        format(date) {
            const newDate = date instanceof Date ? date : typeof date === 'string' ? new Date(date) : new Error('Invalid date parameter passed');

            const formatter = new Intl.DateTimeFormat('en-US', {
                day: '2-digit',
                month: 'short',
                year: 'numeric',
            });

            return formatter.format(newDate);
        },
        formatForInput(date) {
            const newDate = date instanceof Date ? date : typeof date === 'string' ? new Date(date) : new Error('Invalid date parameter passed');

            return newDate.toLocaleDateString('en-CA');
        },
    },
    number: {
        parse(value, defaultValue = undefined) {
            const parsedValue = parseFloat(value);

            if (isNaN(parsedValue)) {
                if (defaultValue === undefined) {
                    throw new Error('No default value provided and the value cannot be parsed into a number');
                }
                return defaultValue;
            }

            return parsedValue;
        }
    }
};