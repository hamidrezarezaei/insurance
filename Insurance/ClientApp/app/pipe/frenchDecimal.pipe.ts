import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: 'frenchDecimal'
})
export class frenchDecimalPipe implements PipeTransform {
    transform(value: number | string, locale?: string): string {
        try {
            let x = value.toString().replace(/,/g, "");
            return new Intl.NumberFormat(locale, {}).format(Number(x));
        }
        catch (e){ return ""; }
    }
}