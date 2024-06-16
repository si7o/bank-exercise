import { test, expect } from '@playwright/test';

const userEmail = 'e2e@test.com';
const userPassword = 'E2Etest123!';

// See here how to get started:
// https://playwright.dev/docs/intro
test('I can create an account and get redirected to the login page', async ({ page }) => {
  await page.goto('/');
  await page.getByLabel('Email').fill(userEmail);
  await page.getByLabel('Password').fill(userPassword);
  await page.getByRole('button', { name: 'Create Account' }).click();


  await expect(page).toHaveURL(/\/login/);
});


test('I can login an get redirected to the Balance page', async ({ page }) => {
  await page.goto('/login');
  await page.getByLabel('Email').fill(userEmail);
  await page.getByLabel('Password').fill(userPassword);
  await page.getByRole('button', { name: 'Login' }).click();

  await expect(page).toHaveURL(/\/balance/);
});

test('I can deposit money and see the updated balance', async ({ page }) => {
  await page.goto('/login');

  await page.getByLabel('Email').fill(userEmail);
  await page.getByLabel('Password').fill(userPassword);
  await page.getByRole('button', { name: 'Login' }).click();

  await expect(page).toHaveURL(/\/balance/);

  await page.locator('#depositAmount').fill('100.12');
  await page.locator('#depositDescription').fill('test deposit');
  await page.getByRole('button', { name: 'Deposit' }).click();

  await expect(page.getByRole('heading', { level: 2 })).toHaveText('Current Balance: 100.12€');
});

test('I can withdraw money and see the updated balance', async ({ page }) => {
  await page.goto('/login');

  await page.getByLabel('Email').fill(userEmail);
  await page.getByLabel('Password').fill(userPassword);
  await page.getByRole('button', { name: 'Login' }).click();

  await expect(page).toHaveURL(/\/balance/);

  await page.locator('#withdrawAmount').fill('100.12');
  await page.locator('#withdrawDescription').fill('test withdraw');
  await page.getByRole('button', { name: 'Withdraw' }).click();

  await expect(page.getByRole('heading', { level: 2 })).toHaveText('Current Balance: 0.00€');
});
