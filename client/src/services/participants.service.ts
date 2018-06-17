import { Participant, ParticipantPost } from "@/shared/models";
import { exec, server } from "./api";

export function getAll() {
  return exec<Participant[]>(server.get("/participants"));
}

export function get(id: string) {
  return exec<Participant>(server.get(`/participants/${id}`));
}

export function add(participant: ParticipantPost) {
  return exec<Participant>(server.post("/participants", participant));
}

export function update(code: string, participant: Participant) {
  return exec<Participant>(server.put(`/participants/${code}`, participant));
}

export function remove(id: string) {
  return exec(server.delete(`/participants/${id}`));
}

export function balance(from: string, to: string) {
  return exec<number>(server.get(`/participants/${from}/balance/${to}`));
}
