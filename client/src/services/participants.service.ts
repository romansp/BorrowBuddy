import { Participant } from "@/shared/models";
import { exec, server } from "./api";

export default class ParticipantsService {
  public static getAll() {
    return exec<Participant[]>(server.get("/participants"));
  }

  public static balance(from: string, to: string) {
    return exec<number>(server.get(`/participants/${from}/balance/${to}`));
  }
}
