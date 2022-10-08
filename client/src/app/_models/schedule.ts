import { ScheduleRecipe } from "./scheduleRecipe";

export interface Schedule {
    scheduleId: number;
    scheduleDate: Date;
    scheduleRecipes: ScheduleRecipe[];
}