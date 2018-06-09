import { Flow, FlowPost } from "@/shared/models";
import { exec, server } from "./api";

export class FlowsService {
  public static getAll() {
    return exec<Flow[]>(server.get("/flows"));
  }

  public static post(flow: FlowPost) {
    return exec(server.post("/flows", flow));
  }
}
