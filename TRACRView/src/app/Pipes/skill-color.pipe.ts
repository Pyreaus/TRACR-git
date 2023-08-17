import { Pipe, PipeTransform } from '@angular/core';
import { Skill } from '../Interfaces/Skill';

@Pipe({
    name: 'skillColor'
})
export class SkillColorPipe implements PipeTransform {
    transform(value: string | undefined, skills: Skill[]): any {
        if (value) {
            let [skillArray, newString]: [string[], string] = [value.split(','), ''];
            skillArray.forEach((skill: string) => {
                let skillObj: Skill | undefined = skills.find((s: Skill) => s.skilL_NAME === skill.trim());
                if (skillObj) {
                    newString += `<span style="background-color:${skillObj.colour};
                    border-radius:5px;font-size:13px!important;
                    font-weight:bold;color:black;padding-bottom:1px;padding-left:2px;
                    padding-right:2px;">${skill}</span> `;
                }
            });
            return newString.trim();
        }
        return null;
    }
}
