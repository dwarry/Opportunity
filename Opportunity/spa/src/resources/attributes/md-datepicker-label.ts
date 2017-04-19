import * as $ from 'jquery';
import { inject } from "aurelia-framework";

@inject(Element)
export class MdDatepickerLabelCustomAttribute {

    constructor(private element: Element) {
    }

    value: string = "";

    attached() {
        var input = $(this.element);
        var div = $("<div class='input-field date-picker'></div>");
        var va = this.element.attributes.getNamedItem("validate");
        if (va)
            div.attr(va.name, va.value);
        input.wrap(div);
        var label = $(`<label class='${input.val() ? "active" : ""}' for='${this.element.id}'>${this.value}</label>`).insertAfter(input);
        var picker = $(this.element).pickadate('picker');
        picker.on("close", () => {
            if (input.val())
                label.addClass("active");
            else
                label.removeClass("active");
        });
    }
}