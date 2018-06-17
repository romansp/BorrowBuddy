import { Currency } from "@/shared/models";
import { exec, server } from "./api";

export function getAll() {
  return exec<Currency[]>(server.get("/currencies"));
}

export function get(code: string) {
  return exec<Currency>(server.get(`/currencies/${code}`));
}

export function add(currency: Currency) {
  return exec<Currency>(server.post("/currencies", currency));
}

export function update(code: string, currency: Currency) {
  return exec<Currency>(server.put(`/currencies/${code}`, currency));
}

export function remove(code: string) {
  return exec(server.delete(`/currencies/${code}`));
}
