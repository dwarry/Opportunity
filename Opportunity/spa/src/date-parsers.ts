import * as moment from 'moment';

export class DmyParser {

    private static readonly _dateRegex = /(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/.compile()

    private static readonly _formats = ["DD/MM/YYYY", "D/M/YYYY", "DD/MM/YY", "D/M/YY"];

    canParse(value): boolean {
        if (value && typeof value === 'string') {
            return DmyParser._dateRegex.test(value);
        }
        return false;
    }

    parse(value): Date {
        debugger;
        for (const format of DmyParser._formats) {
            const m = moment(value, format, true);
            if (m.isValid) { return m.toDate(); }
        }

        return null;
    }

}

export class DmmmmyyyyParser {

    private static parseRegex = /\d{1,2} (?:January|February|March|April|May|June|July|August|September|October|November|December), \d{4}/

    canParse(value): boolean {
        return DmmmmyyyyParser.parseRegex.test(value);
    }

    parse(value): Date {
        return moment(value, "DD MMMM, YYYY", true).toDate();
    }

}